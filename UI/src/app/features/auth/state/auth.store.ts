import { patchState, signalStore, withComputed, withMethods, withState } from '@ngrx/signals';
import { computed, inject } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../service/auth.service';
import { async, firstValueFrom } from 'rxjs';
import { LoginDto } from '../model/login-dto.model';
import { toast } from '@spartan-ng/brain/sonner';
import { JwtPayload } from '../model/jwt-payload.model';
import { withDevtools } from '@angular-architects/ngrx-toolkit';
import { Router } from '@angular/router';

interface IAuthState {
  token: string | null;
  isLoading: boolean;
  error: string | null;
  role: string | null;
  permissions: string[];
  name: string | null;
  image: string | null;
}

const initialState: IAuthState = {
  token: null,
  isLoading: false,
  error: null,
  role: null,
  permissions: [],
  name: null,
  image: null,
};

export const AuthStore = signalStore(
  { providedIn: 'root' },
  withDevtools('AUTH_STORE'),
  withState(() => {
    const cookieService = inject(CookieService);
    const saveToken = cookieService.get('user-token') || null;
    const role = cookieService.get('user-role') || null;
    const name = cookieService.get('user-name') || null;
    const image = cookieService.get('user-image') || null;
    const savedPermissions = cookieService.get('user-permissions');
    let permissions: string[] = [];
    if (savedPermissions) {
      try {
        permissions = JSON.parse(savedPermissions);
      } catch {
        permissions = [];
      }
    }

    return {
      ...initialState,
      token: saveToken,
      role: role,
      permissions: permissions,
      name: name,
      image: image,
    };
  }),
  withComputed((store) => ({
    isStaff: computed(() => store.role() !== 'patient' && store.role() !== ''),
    isPatient: computed(() => store.role() === 'patient'),
    hasPermission: computed(() => (permission: string) => store.permissions().includes(permission)),
  })),
  withMethods(
    (
      store,
      authService = inject(AuthService),
      router = inject(Router),
      cookieService = inject(CookieService),
    ) => ({
      async loginUser(credentials: LoginDto) {
        patchState(store, { isLoading: true, error: null });
        try {
          const response = await firstValueFrom(authService.login(credentials));
          const token = response.token;
          const userData: JwtPayload = authService.decodeToken(token)!;
          if (!userData) {
            toast.error('فشل في تحميل الداتا , اعد المحاوله لاحقا');
            patchState(store, { isLoading: false });
            return;
          }
          const expiresDate = 7;
          cookieService.set('user-token', token, expiresDate, '/');
          cookieService.set('user-role', userData.role, expiresDate, '/');
          cookieService.set('user-name', userData.FullName, expiresDate, '/');
          cookieService.set('user-image', userData.ImageUrl, expiresDate, '/');
          cookieService.set(
            'user-permissions',
            JSON.stringify(userData.Permissions),
            expiresDate,
            '/',
          );
          patchState(store, {
            token: token,
            isLoading: false,
            role: userData.role,
            permissions: userData.Permissions,
            name: userData.FullName,
            image: userData.ImageUrl,
          });
          toast.success(` اهلا بعودتك ${userData.FullName} `);
          if (store.isStaff()) {
            router.navigate(['/staff']);
          } else if (store.isPatient()) {
            router.navigate(['/Patient']);
          }
        } catch (err: any) {
          patchState(store, { isLoading: false, error: err || 'login failed' });
          toast.error('فشل في تسجيل الدخول', {
            description: err.error.message,
          });
          throw err;
        }
      },
      async logoutUser() {
        patchState(store, { isLoading: true, error: null });
        try {
          await firstValueFrom(authService.logout());

          toast.info('تم تسجيل الخروج بنجاح', {
            description: `إلى اللقاء ${store.name()}`,
          });
          cookieService.deleteAll();

          patchState(store, { ...initialState });
          router.navigate(['/auth']);
        } catch (err: any) {
          console.error('Logout failed on server', err);
          toast.error('فشل تسجيل الخروج من السيرفر، ');
        }
      },
    }),
  ),
);
