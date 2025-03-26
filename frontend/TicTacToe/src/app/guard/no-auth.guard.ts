import { inject } from '@angular/core';
import { AuthService } from '../data/auth.service';
import { Router } from '@angular/router';

export const NoAuthGuard = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.isAuth) {
    return router.createUrlTree(['']);
  }

  return true;
};
