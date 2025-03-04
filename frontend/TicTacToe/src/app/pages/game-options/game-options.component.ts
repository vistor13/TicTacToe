import {Component, inject} from '@angular/core';
import {GameService} from '../../data/game.service';
import {Router, RouterLink} from '@angular/router';

@Component({
  selector: 'app-game-options',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './game-options.component.html',
  styleUrl: './game-options.component.scss'
})
export class GameOptionsComponent {
  gameService : GameService= inject(GameService);
  router : Router = inject(Router);

  startGame(isTwoPlayerMode: boolean) {
    this.gameService.startGame(isTwoPlayerMode).subscribe(() => {
      this.router.navigate(['game']);
    });
  }
}
