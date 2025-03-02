import { Routes } from '@angular/router';
import {GameOptionsComponent} from './pages/game-options/game-options.component';
import {GameComponent} from './pages/game/game.component';

export const routes: Routes =
[
  {path : '', component : GameOptionsComponent },
  {path : 'game', component : GameComponent},
];
