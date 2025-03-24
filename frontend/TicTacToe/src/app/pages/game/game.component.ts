import { Component, inject } from '@angular/core';
import { GameService } from '../../data/game.service';
import {GameStateResponse} from '../../interfaces/game.interface';
import {NgForOf, NgIf} from '@angular/common';
import {RouterLink} from '@angular/router';
import {BackComponent} from '../../common-ui/back/back.component';

@Component({
  selector: 'app-game',
  standalone: true,
  templateUrl: './game.component.html',
  imports: [NgForOf, NgIf, RouterLink, BackComponent],
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
