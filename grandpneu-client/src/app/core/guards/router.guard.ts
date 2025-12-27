import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthStorage } from 'src/app/services/auth-storage';

function getRoleFromToken(token: string): string | null {
  try {
    const payload = JSON.parse(
      atob(token.split('.')[1].replace(/-/g, '+').replace(/_/g, '/'))
    );

    return payload.role || payload[
      'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
    ] || null;
  } catch {
    return null;
  }
}

export const roleGuard = (allowedRoles: string[]): CanActivateFn => {
  return () => {

    const authStorage = inject(AuthStorage);
    const router = inject(Router);

    const token = authStorage.getToken();

    if (!token) {
      router.navigate(['/login']);
      return false;
    }

    const role = getRoleFromToken(token);

    if (role && allowedRoles.includes(role)) {
      return true;
    }

    router.navigate(['/']);
    return false;
  };
};
