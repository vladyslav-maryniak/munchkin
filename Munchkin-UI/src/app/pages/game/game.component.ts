import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Character } from 'src/app/models/character';
import { Game } from 'src/app/models/game';
import { Player } from 'src/app/models/player';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { GameService } from 'src/app/services/game.service';
import { SignalrService } from 'src/app/services/signalr.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css'],
})
export class GameComponent implements OnInit, OnDestroy {
  game!: Game;
  player!: Player;

  get character(): Character {
    return (
      this.game?.table.places.find((x) => x.player.id == this.player?.id)
        ?.character ?? ({} as Character)
    );
  }

  private subscription!: Subscription;

  private eventHandlers = new Map<string, (...args: any[]) => Promise<void>>([
    ['GameStateUpdatedEvent', this.onGameStateUpdatedEvent],
  ]);

  constructor(
    private route: ActivatedRoute,
    private gameService: GameService,
    private authService: AuthenticationService,
    private signalrService: SignalrService
  ) {}

  async ngOnInit(): Promise<void> {
    this.player = this.authService.signedPlayer.getValue() ?? ({} as Player);
    const gameId = this.route.snapshot.paramMap.get('game-id') ?? '';
    this.game = await this.gameService.getGame(gameId);

    await this.signalrService.connect();
    this.signalrService.follow(gameId);

    this.subscription = this.signalrService.gameEvents.subscribe(this.onEvent);
  }

  private onEvent = async (event: string): Promise<void> => {
    if (this.eventHandlers.has(event)) {
      await this.eventHandlers.get(event)?.call(this);
      this.game = await this.gameService.getGame(this.game.id);
    }
  };

  async onGameStateUpdatedEvent(): Promise<void> {
    if (this.game) {
      this.game = await this.gameService.getGame(this.game.id);
    }
  }

  async ngOnDestroy(): Promise<void> {
    this.subscription.unsubscribe();
    await this.signalrService.disconnect();
  }
}
