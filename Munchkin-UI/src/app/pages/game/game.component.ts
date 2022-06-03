import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Character } from 'src/app/models/character';
import { Game } from 'src/app/models/game';
import { Player } from 'src/app/models/player';
import { GameService } from 'src/app/services/game.service';
import { SharedDataService } from 'src/app/services/shared-data.service';
import { SignalrService } from 'src/app/services/signalr.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css'],
})
export class GameComponent implements OnInit, OnDestroy {
  game: Game | undefined;
  player: Player | undefined;

  get character(): Character {
    return (
      this.game?.table.places.find((x) => x.player.id == this.player?.id)
        ?.character ?? ({} as Character)
    );
  }

  private subscriptions: Subscription[] = [];

  private eventHandlers = new Map<string, (...args: any[]) => Promise<void>>([
    ['GameStateUpdatedEvent', this.onGameStateUpdatedEvent],
  ]);

  constructor(
    private route: ActivatedRoute,
    private gameService: GameService,
    private signalrService: SignalrService,
    private dataService: SharedDataService
  ) {}

  async ngOnInit(): Promise<void> {
    const gameId = this.route.snapshot.paramMap.get('game-id') ?? '';
    const game = await this.dataService.getGame(gameId);
    this.subscriptions.push(game.subscribe((x: Game) => (this.game = x)));

    this.subscriptions.push(
      this.dataService.getPlayer().subscribe((x: Player) => (this.player = x))
    );

    this.subscriptions.push(
      this.signalrService.gameEvents.subscribe(this.onEvent)
    );

    await this.signalrService.connect();
    this.signalrService.follow(gameId);
  }

  private onEvent = async (event: string): Promise<void> => {
    if (this.eventHandlers.has(event)) {
      await this.eventHandlers.get(event)?.call(this);
    }
  };

  async onGameStateUpdatedEvent(): Promise<void> {
    if (this.game) {
      this.game = await this.gameService.getGame(this.game.id);
    }
  }

  async ngOnDestroy(): Promise<void> {
    this.subscriptions.forEach((x) => x.unsubscribe());
    await this.signalrService.disconnect();
  }
}
