import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router, UrlSerializer } from '@angular/router';
import { Player } from 'src/app/models/player';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { GameService } from 'src/app/services/game.service';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-game-lobby',
  templateUrl: './game-lobby.component.html',
  styleUrls: ['./game-lobby.component.css'],
})
export class GameLobbyComponent implements OnInit, OnDestroy {
  joinLink!: string;
  gameId!: string;
  players: Player[] = [];

  private hubConnection!: signalR.HubConnection;

  constructor(
    private gameService: GameService,
    private authService: AuthenticationService,
    private route: ActivatedRoute,
    private router: Router,
    private serializer: UrlSerializer
  ) {}

  async ngOnInit(): Promise<void> {
    this.gameId = this.route.snapshot.paramMap.get('game-id') ?? '';
    await this.refreshGameLobby();

    const player = (await this.authService.getUser()) ?? ({} as Player);
    this.joinLink = this.buildJoinLink(this.gameId, player?.nickname);

    await this.startHubConnection();
    this.hubConnection.on(this.gameId, this.onPlayerJoinedEvent);

    await this.resolveJoinLink();
  }

  private async refreshGameLobby() {
    const lobby = await this.gameService.getGameLobby(this.gameId);
    this.players = lobby.players;
  }

  private buildJoinLink(gameId: string, inviter: string): string {
    const tree = this.router.createUrlTree(['game', gameId, 'lobby'], {
      queryParams: { inviter },
    });
    const query = this.serializer.serialize(tree);
    return `${window.location.origin}${query}`;
  }

  private async startHubConnection(): Promise<void> {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.hostUrl}/event`)
      .build();
    await this.hubConnection.start();
  }

  private onPlayerJoinedEvent = async (event: string): Promise<void> => {
    if (event === 'PlayerJoinedEvent') {
      await this.refreshGameLobby();
    }
  };

  private async resolveJoinLink(): Promise<void> {
    const inviter = this.route.snapshot.queryParamMap.get('inviter');
    if (inviter) {
      await this.gameService.joinPlayer(this.gameId);
    }
  }

  async ngOnDestroy(): Promise<void> {
    await this.hubConnection.stop();
  }
}
