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
    const endpoint = this.gameControllerUrl;

    const observable = this.httpClient.post<Game>(endpoint, {}, options);
    const response = await firstValueFrom(observable);

    return response.body ?? ({} as Game);
  }

  async getGame(gameId: string): Promise<Game> {
    const endpoint = `${this.gameControllerUrl}/${gameId}`;

    const observable = this.httpClient.get<Game>(endpoint, options);
    const response = await firstValueFrom(observable);

    return response.body ?? ({} as Game);
  }

  async updateGameState(gameId: string, state: string): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/update-state`;
    const body = { state };

    const observable = this.httpClient.post(endpoint, body, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }

  async getGameLobby(gameId: string): Promise<GameLobby> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/lobby`;

    const observable = this.httpClient.get<GameLobby>(endpoint, options);
    const response = await firstValueFrom(observable);

    return response.body ?? ({} as GameLobby);
  }

  async joinPlayer(gameId: string): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/join`;
    const player = this.authService.signedPlayer.getValue();
    const body = { playerId: player?.id };

    const observable = this.httpClient.post<Player>(endpoint, body, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }

  async startGame(gameId: string): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/start-game`;

    const observable = this.httpClient.post(endpoint, {}, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }

  async playCard(
    gameId: string,
    playerId: string,
    cardId: string,
    metadata: Map<string, string> | null
  ): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/play-card`;
    const bodyMetadata = [];
    metadata?.entries();

    const convMap: { [key: string]: string } = {};
    metadata?.forEach((val: string, key: string) => {
      convMap[key] = val;
    });
    const body = { playerId, cardId, metadata: convMap };

    const observable = this.httpClient.post(endpoint, body, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }

  async drawCard(gameId: string, playerId: string): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/draw-card`;
    const body = { playerId };

    const observable = this.httpClient.post(endpoint, body, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }

  async initiateCombat(gameId: string, characterId: string): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/initiate-combat`;
    const body = { characterId };

    const observable = this.httpClient.post(endpoint, body, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }

  async completeCombat(gameId: string): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/complete-combat`;

    const observable = this.httpClient.post(endpoint, {}, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }

  async comeToHelp(gameId: string, characterId: string): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/come-to-help`;
    const body = { characterId };

    const observable = this.httpClient.post(endpoint, body, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }

  async stopAskingForHelp(gameId: string): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/stop-asking-for-help`;

    const observable = this.httpClient.post(endpoint, {}, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }

  async resumeCombat(gameId: string, characterId: string): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/resume-combat`;
    const body = { characterId };

    const observable = this.httpClient.post(endpoint, body, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }

  async rollDie(gameId: string, playerId: string): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/roll-die`;
    const body = { playerId };

    const observable = this.httpClient.post(endpoint, body, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }

  async resolveRunAwayRoll(
    gameId: string,
    characterId: string
  ): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/resolve-run-away-roll`;
    const body = { characterId };

    const observable = this.httpClient.post(endpoint, body, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }
}
