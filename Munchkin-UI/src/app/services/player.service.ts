import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PlayerGame } from '../models/player-game';

const options = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  observe: 'response' as const,
  responseType: 'json' as const,
  withCredentials: true,
};

@Injectable({
  providedIn: 'root',
})
export class PlayerService {
  private playerControllerUrl = `${environment.hostUrl}/api/players`;

  constructor(private httpClient: HttpClient) {}

  async getPlayerGames(playerId: string): Promise<PlayerGame[]> {
    const endpoint: string = `${this.playerControllerUrl}/${playerId}/games`;
    const observable = this.httpClient.get<PlayerGame[]>(endpoint, options);
    const response = await firstValueFrom(observable);

    return response.body ?? [];
  }
}
