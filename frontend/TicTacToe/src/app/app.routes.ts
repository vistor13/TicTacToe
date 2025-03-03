import { Routes } from '@angular/router';
import {GameOptionsComponent} from './pages/game-options/game-options.component';
import {GameComponent} from './pages/game/game.component';
import {LayoutComponent} from './common-ui/layout/layout.component';
import {InfoComponent} from './pages/info/info.component';

export const routes: Routes =
[
  {path: '', component: LayoutComponent, children: [
      {path : '', component : GameOptionsComponent },
      {path : 'game', component : GameComponent}
    ]}
];
