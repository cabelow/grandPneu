import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { UsersComponent } from './pages/users/users.component';
import { roleGuard } from './core/guards/router.guard';
import { authGuard, } from './core/guards/auth.guard';
import { RegisterUserComponent } from './pages/register-user/register-user.component';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./pages/login/login.component').then((m) => m.LoginComponent),
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterUserComponent },
  {
    path: 'home',
    loadComponent: () =>
      import('./pages/home/home.page')
        .then(m => m.HomePage),
    canActivate: [
      authGuard,
      roleGuard(['Admin', 'Gestor'])
    ]
  },
  {
    path: 'users',
    loadComponent: () =>
      import('./pages/users/users.component')
        .then(m => m.UsersComponent),
    canActivate: [
      authGuard,
      roleGuard(['Admin', 'Gestor'])
    ]
  }

];
