import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { firstValueFrom, lastValueFrom, Subscription } from 'rxjs';
import { Game } from 'src/app/models/game';
import { MunchkinCard } from 'src/app/models/munchkin-card';
import { Player } from 'src/app/models/player';
import { CardService } from 'src/app/services/card.service';
import { GameService } from 'src/app/services/game.service';
import { SharedDataService } from 'src/app/services/shared-data.service';
import { SignalrService } from 'src/app/services/signalr.service';
import {
  MetadataDialogComponent,
  MetaDialogData,
} from '../metadata-dialog/metadata-dialog.component';

@Component({
  selector: 'app-in-hand-card-panel',
  templateUrl: './in-hand-card-panel.component.html',
  styleUrls: ['./in-hand-card-panel.component.css'],
})
export class InHandCardPanelComponent implements OnInit, OnDestroy {
  game!: Game;
  player!: Player;

  private subscriptions: Subscription[] = [];

  multiselect: boolean = false;
  selectedCards: MunchkinCard[] = [];

  get totalGoldPieces(): number {
    return this.selectedCards
      .map((x) => x.goldPieces ?? 0)
      ?.reduce((total, x) => total + x);
  }

  get inHandCards(): MunchkinCard[] {
    const place = this.game?.table.places.find(
      (x) => x.player.id === this.player.id
    );
    return place?.inHandCards ?? [];
  }

  private eventHandlers = new Map<string, (...args: any[]) => Promise<void>>([
    ['ItemCardPlayedEvent', this.onItemCardPlayedEvent],
    ['OneShotCardPlayedEvent', this.onOneShotCardPlayedEvent],
    ['GoUpLevelCardPlayedEvent', this.onGoUpLevelCardPlayedEvent],
    ['MonsterEnhancerCardPlayedEvent', this.onMonsterEnhancerCardPlayedEvent],
    ['PlayerSoldCardsEvent', this.onPlayerSoldCardsEvent],
  ]);

  constructor(
    private route: ActivatedRoute,
    private gameService: GameService,
    private cardService: CardService,
    private signalrService: SignalrService,
    private dataService: SharedDataService,
    public dialog: MatDialog
  ) {}

  async ngOnInit(): Promise<void> {
    const gameId = this.route.snapshot.paramMap.get('game-id') ?? '';
    const game = await this.dataService.getGame(gameId);
    this.subscriptions.push(game.subscribe((x: Game) => (this.game = x)));

    this.subscriptions.push(
      this.signalrService.gameEvents.subscribe(this.onEvent)
    );

    this.subscriptions.push(
      this.dataService.getPlayer().subscribe((x: Player) => (this.player = x))
    );
  }

  private onEvent = async (event: string): Promise<void> => {
    if (this.eventHandlers.has(event)) {
      await this.eventHandlers.get(event)?.call(this);
      this.game = await this.gameService.getGame(this.game.id);
      this.dataService.setGame(this.game);
    }
  };

  async playCard(card: MunchkinCard): Promise<void> {
    let metadata = card.metadata;

    if (card.metadata) {
      metadata = new Map<string, string>(Object.entries(card.metadata));

      for (var key of metadata.keys()) {
        switch (key) {
          case 'monsterCardId':
            const monsterId =
              this.game.table.combatField.monsterSquad[0]?.id ??
              '00000000-0000-0000-0000-000000000000';
            metadata.set(key, monsterId);
            break;

          case 'cursedCharacterId':
            const places = this.game.table.places.filter(
              (x) => x.player.id !== this.player.id
            );
            metadata.set(key, places[0].character.id);
            break;

          case 'dieValue':
            const dieValue = await this.getDieValue();
            metadata.set(key, dieValue);
            break;
        }
      }
    }

    await firstValueFrom(
      this.gameService.playCard(this.game.id, this.player.id, card.id, metadata)
    );
  }

  private getDieValue(): Promise<string> {
    const data = { key: 'dieValue' } as MetaDialogData;
    const dialogRef = this.dialog.open(MetadataDialogComponent, { data });
    return lastValueFrom(dialogRef.afterClosed());
  }

  async sellCards(): Promise<void> {
    const cardIds = this.selectedCards.map((x) => x.id);
    this.clearSelectedCards();
    await firstValueFrom(
      this.gameService.sellCards(this.game.id, this.player.id, cardIds)
    );
  }

  async onItemCardPlayedEvent(): Promise<void> {}

  async onOneShotCardPlayedEvent(): Promise<void> {}

  async onGoUpLevelCardPlayedEvent(): Promise<void> {}

  async onMonsterEnhancerCardPlayedEvent(): Promise<void> {}

  async onPlayerSoldCardsEvent(): Promise<void> {}

  onClick(card: MunchkinCard): void {
    this.isCardSelected(card) ? this.deselectCard(card) : this.selectCard(card);
  }

  isCardSelected(card: MunchkinCard): boolean {
    return this.selectedCards.includes(card);
  }

  selectCard(card: MunchkinCard): void {
    if (!this.cardService.isSaleable(card) && this.multiselect) {
      return;
    }

    if (this.multiselect === false) {
      this.clearSelectedCards();
    }
    this.selectedCards.push(card);
  }

  deselectCard(card: MunchkinCard): void {
    if (this.selectedCards.length === 1) {
      this.multiselect = false;
    }
    const index = this.selectedCards.indexOf(card);
    this.selectedCards.splice(index, 1);
  }

  clearSelectedCards(): void {
    this.multiselect = false;
    this.selectedCards = [];
  }

  switchSelectMode(): void {
    if (!this.isPlayerInCombat()) {
      this.multiselect = !this.multiselect;
    }
  }

  isPlayerInCombat(): boolean {
    return this.gameService.isPlayerInCombat(this.game, this.player);
  }

  getCardDescription(card: MunchkinCard): string {
    return this.cardService.getCardDescription(card);
  }

  getCardImageSrc(card: MunchkinCard): string {
    return this.cardService.getCardImageSrc(card);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((x) => x.unsubscribe());
  }
}
