import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router, UrlSerializer } from '@angular/router';
import { Subscription } from 'rxjs';
import { Player } from 'src/app/models/player';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { GameService } from 'src/app/services/game.service';
import { SignalrService } from 'src/app/services/signalr.service';

@Component({
  selector: 'app-game-lobby',
  templateUrl: './game-lobby.component.html',
  styleUrls: ['./game-lobby.component.css'],
})
export class GameLobbyComponent implements OnInit, OnDestroy {
  joinLink!: string;
  gameId!: string;
  players: Player[] = [];
  subscription!: Subscription;

  constructor(
    private gameService: GameService,
    private signalrService: SignalrService,
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

    await this.signalrService.connect();
    this.signalrService.follow(this.gameId);
    this.subscription = this.signalrService.gameEvents.subscribe(this.onEvent);

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

  private onEvent = async (event: string): Promise<void> => {
    if (event === 'PlayerJoinedEvent') {
      await this.refreshGameLobby();
    }
    if (event === 'GameStartedEvent') {
      await this.router.navigate(['game', this.gameId]);
    }
  };

  private async resolveJoinLink(): Promise<void> {
    const inviter = this.route.snapshot.queryParamMap.get('inviter');
    if (inviter) {
      await this.gameService.joinPlayer(this.gameId);
    }
  }

  async goBack() {
    await this.router.navigate(['home']);
  }

  async startGame() {
    await this.gameService.startGame(this.gameId);
  }

  async ngOnDestroy(): Promise<void> {
    this.subscription.unsubscribe();
    await this.signalrService.disconnect();
  }
}
