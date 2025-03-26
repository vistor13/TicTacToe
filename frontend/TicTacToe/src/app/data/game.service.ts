import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

import {tap} from 'rxjs';
import {CookieService} from 'ngx-cookie-service';
import {StartGameResponse} from '../interfaces/Game/startgame.interface';
import {GameStateResponse} from '../interfaces/Game/gamestate.interface';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  http : HttpClient = inject(HttpClient);
  private cookieService = inject(CookieService);
  private apiUrl = 'http://localhost:5120/api/game';

  startGame(isTwoPlayerMode: boolean) {
    return this.http.post<StartGameResponse>(`${this.apiUrl}/start`, null, {
      params: { isTwoPlayerMode: isTwoPlayerMode.toString() },
    }).pipe(
      tap(res => {
        this.cookieService.set("gameId", res.id.toString());
      })
    );
  }

  getGame() {
    const gameId = this.getGameId();
    return this.http.get<GameStateResponse>(`${this.apiUrl}/state`, {
      params: { gameId: gameId.toString() }
    });
  }

  getGameId(): number {
    return Number(this.cookieService.get("gameId"));
  }

  makeMove(row: number, col: number) {
    const gameId = this.getGameId();
    return this.http.post<GameStateResponse>(`${this.apiUrl}/move`, {
      gameId: gameId,
      row: row,
      col: col
    });
  }

}
