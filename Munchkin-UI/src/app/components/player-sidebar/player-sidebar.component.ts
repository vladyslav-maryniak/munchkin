import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { lastValueFrom, Subscription } from 'rxjs';
import { Equipment } from 'src/app/models/equipment';
import { Game } from 'src/app/models/game';
import { Place } from 'src/app/models/place';
import { Player } from 'src/app/models/player';
import { Bribe } from 'src/app/models/bribe';
import { Trade } from 'src/app/models/trade';
import { Reward } from 'src/app/models/reward';
import { CardService } from 'src/app/services/card.service';
import { GameService } from 'src/app/services/game.service';
import { SharedDataService } from 'src/app/services/shared-data.service';
import { ConnectionPositionPair } from '@angular/cdk/overlay';
import { Offer } from 'src/app/models/offer';
import { SignalrService } from 'src/app/services/signalr.service';
import { MatDialog } from '@angular/material/dialog';
import {
  BribeDialogComponent,
  BribeDialogData,
} from '../bribe-dialog/bribe-dialog.component';
import {
  TradeDialogComponent,
  TradeDialogData,
} from '../trade-dialog/trade-dialog.component';

@Component({
  selector: 'app-player-sidebar',
  templateUrl: './player-sidebar.component.html',
  styleUrls: ['./player-sidebar.component.css'],
})
export class PlayerSidebarComponent implements OnInit, OnDestroy {
  game!: Game;
  player!: Player;

  private decisionMessageOffsetX = -3;
  private decisionMessageOffsetY = -8;
  decisionMessagePosition = [
    new ConnectionPositionPair(
      { originX: 'start', originY: 'bottom' },
      { overlayX: 'end', overlayY: 'top' },
      this.decisionMessageOffsetX,
      this.decisionMessageOffsetY
    ),
  ];

  private offerMenuOffsetX = -5;
  offerMenuPosition = [
    new ConnectionPositionPair(
      { originX: 'start', originY: 'center' },
      { overlayX: 'end', overlayY: 'center' },
      this.offerMenuOffsetX
    ),
  ];

  selectedPlace!: Place | undefined;
  showPlace: boolean = false;

  private subscriptions: Subscription[] = [];

  get places(): Place[] {
    return this.game?.table.places;
  }

  private eventHandlers = new Map<string, (...args: any[]) => Promise<void>>([
    ['PlayerAcceptedOfferEvent', this.onPlayerAcceptedOfferEvent],
    ['PlayerDeclinedOfferEvent', this.onPlayerDeclinedOfferEvent],
    ['PlayerOfferedBribeEvent', this.onPlayerOfferedBribeEvent],
    ['PlayerOfferedTradeEvent', this.onPlayerOfferedTradeEvent],
    ['PlayerOfferedRewardEvent', this.onPlayerOfferedRewardEvent],
  ]);

  constructor(
    private route: ActivatedRoute,
    private cardService: CardService,
    private dataService: SharedDataService,
    private gameService: GameService,
    private signalrService: SignalrService,
    public dialog: MatDialog
  ) {}

  async ngOnInit(): Promise<void> {
    const gameId = this.route.snapshot.paramMap.get('game-id') ?? '';
    const game = await this.dataService.getGame(gameId);
    this.subscriptions.push(game.subscribe((x: Game) => (this.game = x)));
    const player = this.dataService.getPlayer();
    this.subscriptions.push(player.subscribe((x: Player) => (this.player = x)));

    this.subscriptions.push(
      this.signalrService.gameEvents.subscribe(this.onEvent)
    );
  }

  private onEvent = async (event: string): Promise<void> => {
    if (this.eventHandlers.has(event)) {
      await this.eventHandlers.get(event)?.call(this);
      this.game = await this.gameService.getGame(this.game.id);
      this.dataService.setGame(this.game);
    }
  };

  async onPlayerAcceptedOfferEvent(): Promise<void> {}
  async onPlayerDeclinedOfferEvent(): Promise<void> {}
  async onPlayerOfferedBribeEvent(): Promise<void> {}
  async onPlayerOfferedTradeEvent(): Promise<void> {}
  async onPlayerOfferedRewardEvent(): Promise<void> {}

  clickOnPlace(place: Place): void {
    if (place.player.id !== this.player.id) {
      this.selectedPlace = this.selectedPlace === place ? undefined : place;
    }
  }

  async acceptOffer(offer: Offer): Promise<void> {
    await this.gameService.acceptOffer(
      this.game.id,
      offer.id,
      offer.offerorId,
      offer.offereeId
    );
  }

  async declineOffer(offer: Offer): Promise<void> {
    await this.gameService.declineOffer(
      this.game.id,
      offer.id,
      offer.offerorId,
      offer.offereeId
    );
  }

  async onBribe(offeree: Place): Promise<void> {
    const offeror = this.places.find((x) => x.player.id === this.player.id);
    if (offeror === undefined) return;

    const data = {
      agreement: '',
      inHandCards: offeror.inHandCards,
      selectedCards: [],
    } as BribeDialogData;

    const dialogRef = this.dialog.open(BribeDialogComponent, { data });
    const result: BribeDialogData = await lastValueFrom(
      dialogRef.afterClosed()
    );
    if (result === undefined) return;

    await this.gameService.offerBribe(
      this.game.id,
      offeror.player.id,
      offeree.player.id,
      result.agreement,
      result.selectedCards.map((x) => x.id)
    );
    this.selectedPlace = undefined;
  }

  async onTrade(offeree: Place): Promise<void> {
    const offeror = this.places.find((x) => x.player.id === this.player.id);
    if (offeror === undefined) return;

    const data = {
      offerorEquipment: offeror.character.equipment,
      offereeEquipment: offeree.character.equipment,
      demand: [],
      supply: [],
    } as TradeDialogData;

    const dialogRef = this.dialog.open(TradeDialogComponent, { data });
    const result: TradeDialogData = await lastValueFrom(
      dialogRef.afterClosed()
    );
    if (result === undefined) return;

    await this.gameService.offerTrade(
      this.game.id,
      offeror.player.id,
      offeree.player.id,
      result.supply.map((x) => x.id),
      result.demand.map((x) => x.id)
    );
    this.selectedPlace = undefined;
  }

  getFirstIncomingOffer(offeror: Place): Offer | undefined {
    if (this.game.table.offers.length > 0) {
      const filteredOffers = this.game.table.offers.filter(
        (x) =>
          x.offereeId === this.player.id && x.offerorId === offeror.player.id
      );
      return filteredOffers[0];
    }
    const reward = this.game.table.combatField.reward;
    if (reward !== null) {
      if (
        reward.offerorId !== this.player.id &&
        reward.offereeId !== this.player.id &&
        reward.offerorId === offeror.player.id
      ) {
        return reward;
      }
    }
    return undefined;
  }

  getOfferDescription(offer: Offer): string {
    if (this.isBribe(offer)) {
      return this.getBribeDescription(offer);
    }
    if (this.isTrade(offer)) {
      return this.getTradeDescription(offer);
    }
    if (this.isReward(offer)) {
      return this.getRewardDescription(offer);
    }
    return '';
  }

  getBribeDescription(offer: Bribe): string {
    const offerorCards = this.getInHandCardNames(
      offer.itemCardIds,
      offer.offerorId
    );
    const hasAgreement = offer.agreement.trim().length > 0;

    let output = `I'll give you my ${offerorCards}`;
    if (hasAgreement) {
      output += ` if you ${offer.agreement}`;
    }
    output += '.';

    return output;
  }

  getTradeDescription(offer: Trade): string {
    const offerorCards = this.getEquipmentCardName(
      offer.offerorItemCardIds,
      offer.offerorId
    );
    const hasOfferorCards = offerorCards.length > 0;

    const offereeCards = this.getEquipmentCardName(
      offer.offereeItemCardIds,
      offer.offereeId
    );
    const hasOffereeCards = offereeCards.length > 0;

    let output = "Let's trade";
    if (hasOfferorCards) {
      output += ` my ${offerorCards}`;
      if (hasOffereeCards) {
        output += ` for`;
      }
    }
    if (hasOffereeCards) {
      output += ` yours ${offereeCards}`;
    }
    output += '.';

    return output;
  }

  getRewardDescription(offer: Reward): string {
    const cardsForTransfer = this.getInHandCardNames(
      offer.itemCardIds,
      offer.offerorId
    );
    const hasCardsForTransfer = cardsForTransfer.length > 0;

    const cardsForPlay = this.getInHandCardNames(
      offer.cardIdsForPlay,
      offer.offerorId
    );
    const hasCardsForPlay = cardsForPlay.length > 0;
    const hasTreasures = offer.numberOfTreasures > 0;

    let output = '';
    if (hasCardsForTransfer || hasTreasures) {
      output += "I'll give you ";
      output += cardsForTransfer;
      if (hasCardsForTransfer && hasTreasures) {
        output += ', ';
      }
      output += `${offer.numberOfTreasures} treasures (${
        offer.helperPicksFirst ? 'you' : 'I'
      } pick first)`;
      if (hasCardsForPlay) {
        output += ' and ';
      }
    }
    if (hasCardsForPlay) {
      output += `I'll play ${cardsForPlay} on you`;
    }
    output += '.';

    return output;
  }

  getInHandCardNames(cardIds: string[], playerId: string): string {
    return (
      this.game.table.places
        .find((x) => x.player.id === playerId)
        ?.inHandCards.filter((x) => cardIds.includes(x.id))
        .map((x) => x.name) ?? []
    ).join(', ');
  }

  getEquipmentCardName(cardIds: string[], playerId: string): string {
    const equipment = this.game.table.places.find(
      (x) => x.player.id === playerId
    )?.character.equipment;

    if (equipment === undefined) return '';

    const array = [
      equipment.headgear,
      equipment.armor,
      equipment.footgear,
      equipment.leftHand,
    ];
    if (equipment.rightHand?.id !== equipment.leftHand?.id) {
      array.push(equipment.rightHand);
    }
    return array
      .filter((x) => cardIds.includes(x?.id))
      .map((x) => x.name)
      .join(', ');
  }

  isBribe(object: Object): object is Bribe {
    return Object.prototype.hasOwnProperty.call(object, 'agreement');
  }

  isTrade(object: Object): object is Trade {
    return Object.prototype.hasOwnProperty.call(object, 'offerorItemCardIds');
  }

  isReward(object: Object): object is Reward {
    return Object.prototype.hasOwnProperty.call(object, 'helperPicksFirst');
  }

  isPlayerTurn(player: Player): boolean {
    return this.gameService.isPlayerTurn(this.game, player);
  }

  getNicknameAbbreviation(player: Player): string {
    return this.cardService.getNicknameAbbreviation(player);
  }

  getCharacterEquipmentDescription(equipment: Equipment): string {
    return this.cardService.getCharacterEquipmentDescription(equipment);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((x) => x.unsubscribe());
  }
}
