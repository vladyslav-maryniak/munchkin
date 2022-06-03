import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Game } from '../models/game';
import { Player } from '../models/player';
import { AuthenticationService } from './authentication.service';
import { GameService } from './game.service';

@Injectable({
  providedIn: 'root',
})
export class SharedDataService {
  private gameSubject!: BehaviorSubject<Game>;
  private playerSubject!: BehaviorSubject<Player>;

  constructor(
    private gameService: GameService,
    private authService: AuthenticationService
  ) {}

  setGame(game: Game) {
    this.gameSubject.next(game);
  }

  async getGame(gameId: string): Promise<Observable<Game>> {
    if (
      this.gameSubject === undefined ||
      this.gameSubject.getValue().id !== gameId
    ) {
      const game = await this.gameService.getGame(gameId);
      this.gameSubject = new BehaviorSubject<Game>(game);
    }
    return this.gameSubject.asObservable();
  }

  setPlayer(player: Player) {
    this.playerSubject.next(player);
  }

  getPlayer(): Observable<Player> {
    if (this.playerSubject === undefined) {
      const player = this.authService.signedPlayer.getValue() ?? ({} as Player);
      this.playerSubject = new BehaviorSubject<Player>(player);
    }
    return this.playerSubject.asObservable();
  }
}
