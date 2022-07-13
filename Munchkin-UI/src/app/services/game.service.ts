import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
  HttpResponse,
} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { catchError, firstValueFrom, Observable } from 'rxjs';
import { Game } from '../models/game';
import { Player } from '../models/player';
import { AuthenticationService } from './authentication.service';
import { GameLobby } from '../models/game-lobby';
import { WaitingState } from '../game-states/combat-field/waiting-state';
import { MatSnackBar } from '@angular/material/snack-bar';

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
    private authService: AuthenticationService,
    private matSnackBar: MatSnackBar
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

  playCard(
    gameId: string,
    playerId: string,
    cardId: string,
    metadata: Map<string, string> | null
  ): Observable<HttpResponse<Object>> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/play-card`;
    metadata?.entries();

    const convMap: { [key: string]: string } = {};
    metadata?.forEach((val: string, key: string) => {
      convMap[key] = val;
    });
    const body = { playerId, cardId, metadata: convMap };

    return this.httpClient.post(endpoint, body, options).pipe(
      catchError((response: HttpErrorResponse) => {
        return this.handleHttpError(response, this.matSnackBar);
      })
    );
  }

  drawCard(gameId: string, playerId: string): Observable<HttpResponse<Object>> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/draw-card`;
    const body = { playerId };

    return this.httpClient.post(endpoint, body, options).pipe(
      catchError((response: HttpErrorResponse) => {
        return this.handleHttpError(response, this.matSnackBar);
      })
    );
  }

  async applyCurse(gameId: string, characterId: string): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/apply-curse`;
    const body = { characterId };

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

  sellCards(
    gameId: string,
    playerId: string,
    cardIds: string[]
  ): Observable<HttpResponse<Object>> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/sell-cards`;
    const body = { playerId, cardIds };

    return this.httpClient.post(endpoint, body, options).pipe(
      catchError((response: HttpErrorResponse) => {
        return this.handleHttpError(response, this.matSnackBar);
      })
    );
  }

  async acceptOffer(
    gameId: string,
    offerId: string,
    offerorId: string,
    offereeId: string
  ): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/accept-offer`;
    const body = { offerId, offerorId, offereeId };

    const observable = this.httpClient.post(endpoint, body, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }

  async declineOffer(
    gameId: string,
    offerId: string,
    offerorId: string,
    offereeId: string
  ): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/decline-offer`;
    const body = { offerId, offerorId, offereeId };

    const observable = this.httpClient.post(endpoint, body, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }

  async offerReward(
    gameId: string,
    offerorId: string,
    itemCardIds: string[],
    cardIdsForPlay: string[],
    numberOfTreasures: number,
    helperPicksFirst: boolean
  ): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/offer-reward`;
    const body = {
      offerorId,
      itemCardIds,
      cardIdsForPlay,
      numberOfTreasures,
      helperPicksFirst,
    };

    const observable = this.httpClient.post(endpoint, body, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }

  async offerBribe(
    gameId: string,
    offerorId: string,
    offereeId: string,
    agreement: string,
    itemCardIds: string[]
  ): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/offer-bribe`;
    const body = {
      offerorId,
      offereeId,
      agreement,
      itemCardIds,
    };

    const observable = this.httpClient.post(endpoint, body, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }

  async offerTrade(
    gameId: string,
    offerorId: string,
    offereeId: string,
    offerorItemCardIds: string[],
    offereeItemCardIds: string[]
  ): Promise<boolean> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/offer-trade`;
    const body = {
      offerorId,
      offereeId,
      offerorItemCardIds,
      offereeItemCardIds,
    };

    const observable = this.httpClient.post(endpoint, body, options);
    const response = await firstValueFrom(observable);

    return response.ok;
  }

  lootRoom(gameId: string, playerId: string): Observable<HttpResponse<Object>> {
    const endpoint = `${this.gameControllerUrl}/${gameId}/loot-room`;
    const body = { playerId };

    return this.httpClient.post(endpoint, body, options).pipe(
      catchError((response: HttpErrorResponse) => {
        return this.handleHttpError(response, this.matSnackBar);
      })
    );
  }

  handleHttpError(
    response: HttpErrorResponse,
    matSnackBar: MatSnackBar
  ): Observable<never> {
    matSnackBar.open(response.error.errorMessage, undefined, {
      duration: 3000,
      verticalPosition: 'top',
    });
    throw response;
  }

  isPlayerInCombat(game: Game, player: Player): boolean {
    const place = game.table.places.find((x) => x.player.id === player.id);
    if (place) {
      if (
        game.table.combatField.characterSquad.find(
          (x) => x.id === place.character.id
        )
      )
        return true;
    }
    return false;
  }

  isPlayerTurn(game: Game, player: Player): boolean {
    const index = game.turnIndex % game.table.places.length;
    return game.table.places[index].player.id == player.id;
  }

  isInCombatState(game: Game) {
    return game.state !== WaitingState.name;
  }
}
