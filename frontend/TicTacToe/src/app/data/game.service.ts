import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  http : HttpClient = inject(HttpClient);

  private apiUrl = 'http://localhost:5120/api/game/start';
  startGame(isTwoPlayerMode: boolean) {
    return this.http.post<void>(this.apiUrl, null, {
      params: { isTwoPlayerMode: isTwoPlayerMode.toString() },
    });
  }
}
