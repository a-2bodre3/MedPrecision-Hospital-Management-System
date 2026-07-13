import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthStore } from '../../features/auth/state/auth.store';
import { toast } from '@spartan-ng/brain/sonner';

export const authGuard: CanActivateFn = (route, state) => {
  const authStore = inject(AuthStore);
  const router = inject(Router);
  if (authStore.token()) return true;
  toast.error('غير مسموح', {
    description: 'عذراً، يجب تسجيل الدخول أولاً!',
  });
  router.navigate(['/auth']);
  return false;
};
