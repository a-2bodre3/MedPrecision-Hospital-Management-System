import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthStore } from '../../features/auth/state/auth.store';
import { toast } from '@spartan-ng/brain/sonner';

export const permissionGuard: CanActivateFn = (route, state) => {
  const authStore = inject(AuthStore);
  const router = inject(Router);

  const requiredPermission = route.data['permission'] as string;
  const userPermissions = authStore.permissions(); // مصفوفة الصلاحيات في الستور

  if (requiredPermission && userPermissions.includes(requiredPermission)) {
    return true;
  }

  toast.warning('وصول مرفوض', {
    description: 'عذراً، لا تمتلك الصلاحية الكافية لعرض هذه الشاشة.',
  });

  router.navigate(['/staff']);
  return false;
};
