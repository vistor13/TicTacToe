import { Component, inject } from '@angular/core';
import { GameService } from '../../data/game.service';
import {NgForOf, NgIf} from '@angular/common';
import {BackComponent} from '../../common-ui/back/back.component';
import {GameStateResponse} from '../../interfaces/Game/gamestate.interface';

@Component({
  selector: 'app-game',
  standalone: true,
  templateUrl: './game.component.html',
  imports: [NgForOf, NgIf, BackComponent],
  styleUrl: './game.component.scss'
})
export class GameComponent {
  gameService: GameService = inject(GameService);
  gameState!: GameStateResponse;

  constructor() {
    this.loadGameState();
  }

  loadGameState() {
    this.gameService.getGame().subscribe(
      (response) => {
        this.gameState = response;
      }
    );
  }

  makeMove(row: number, col: number) {
    this.gameService.makeMove(row, col)
      .subscribe(() => {
        this.loadGameState();
      }
    );
  }
}
