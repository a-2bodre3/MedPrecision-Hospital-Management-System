import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthStore } from '../../features/auth/state/auth.store';
import { toast } from '@spartan-ng/brain/sonner';

export const roleGuard: CanActivateFn = (route, state) => {
  const authStore = inject(AuthStore);
  const router = inject(Router);

  const role = (authStore.role() as string)?.toLowerCase();
  const expectedPortal = (route.data['portal'] as string)?.toLowerCase();
  if (!role) {
    return true;
  }
  if (expectedPortal === 'Patient' && role === 'Patient') {
    return true;
  }

  if (expectedPortal === 'staff' && role !== 'Patient') {
    return true;
  }

  if (role === 'Patient' && expectedPortal === 'staff') {
    router.navigate(['/Patient']);
    return false; // بنقفل الدالة هنا صامتاً بدون توست
  }

  toast.warning('وصول مرفوض', {
    description: 'ليس لديك صلاحية لدخول هذه الصفحة.',
  });

  if (role === 'Patient') {
    router.navigate(['/Patient']);
  } else {
    router.navigate(['/staff']);
  }

  return false;
};
