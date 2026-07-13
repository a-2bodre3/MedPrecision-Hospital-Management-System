import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthStore } from '../../features/auth/state/auth.store';

export const guestGuard: CanActivateFn = (route, state) => {
  const authStore = inject(AuthStore);
  const router = inject(Router);
  if (authStore.token()) {
    const role = authStore.role();
    if (role === 'Patient') {
      router.navigate(['/Patient']);
    } else {
      router.navigate(['/staff']);
    }
    return false;
  }
  return true;
};
