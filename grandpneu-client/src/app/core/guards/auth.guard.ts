import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthStorage } from 'src/app/services/auth-storage';

export const authGuard: CanActivateFn = () => {
  const authStorage = inject(AuthStorage);
  const router = inject(Router);

  if (authStorage.isAuthenticated()) {
    return true;
  }

  router.navigate(['/login']);
  return false;
};
