import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { firstValueFrom } from 'rxjs';
import { Game } from '../models/game';
import { Player } from '../models/player';
import { AuthenticationService } from './authentication.service';
import { GameLobby } from '../models/game-lobby';

const options = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  observe: 'response' as const,
  responseType: 'json' as const,
  withCredentials: true,
};

@Injectable({
  providedIn: 'root',
})
export class GameService {
  private gameControllerUrl = `${environment.hostUrl}/api/games`;

  constructor(
    private httpClient: HttpClient,
    private authService: AuthenticationService
  ) {}

  async createGame(): Promise<Game> {
    const observable = this.httpClient.post<Game>(
      this.gameControllerUrl,
      {},
      options
    );
    const response = await firstValueFrom(observable);

    return response.body ?? ({} as Game);
  }

  async getGame(gameId: string): Promise<Game> {
    const endpoint: string = `${this.gameControllerUrl}/${gameId}`;

    const observable = this.httpClient.get<Game>(endpoint, options);
    const response = await firstValueFrom(observable);
    return response.body ?? ({} as Game);
  }

  async getGameLobby(gameId: string): Promise<GameLobby> {
    const endpoint: string = `${this.gameControllerUrl}/${gameId}/lobby`;

    const observable = this.httpClient.get<GameLobby>(endpoint, options);
    const response = await firstValueFrom(observable);
    return response.body ?? ({} as GameLobby);
  }

  async joinPlayer(gameId: string): Promise<boolean> {
    const endpoint: string = `${this.gameControllerUrl}/${gameId}/join`;
    const player = this.authService.signedPlayer.getValue();
    const body = { playerId: player?.id };

    const observable = this.httpClient.post<Player>(endpoint, body, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }
}
