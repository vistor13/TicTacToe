import { Routes } from '@angular/router';
import {GameOptionsComponent} from './pages/game-options/game-options.component';
import {GameComponent} from './pages/game/game.component';
import {LayoutComponent} from './common-ui/layout/layout.component';
import {InfoComponent} from './pages/info/info.component';
import {LoginComponent} from './pages/login/login.component';
import {RegisterComponent} from './pages/register/register.component';
import {NoAuthGuard} from './guard/no-auth.guard';
import {AuthGuard} from './guard/auth.guard';

export const routes: Routes =
[
  {path: '', component: LayoutComponent, children: [
      {path : '', component : GameOptionsComponent },
      {path : 'game', component : GameComponent},
      {path : 'info', component : InfoComponent}
    ],canActivate:[AuthGuard]
  },
  {path: 'login',component: LoginComponent,canActivate:[NoAuthGuard]},
  {path: 'signup', component: RegisterComponent,canActivate:[NoAuthGuard]}
];
