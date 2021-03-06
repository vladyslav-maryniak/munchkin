import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { lastValueFrom, Subscription } from 'rxjs';
import { ActionControlAreaDirective } from 'src/app/directives/action-control-area.directive';
import { GameContext } from 'src/app/game-states/base/game-context';
import { GameState } from 'src/app/game-states/base/game-state';
import { AskingForHelpState } from 'src/app/game-states/combat-field/asking-for-help-state';
import { BadStuffApplicationState } from 'src/app/game-states/combat-field/bad-stuff-application-state';
import { CombatCompletionState } from 'src/app/game-states/combat-field/combat-completion-state';
import { CombatInitiationState } from 'src/app/game-states/combat-field/combat-initiation-state';
import { CombatResumptionState } from 'src/app/game-states/combat-field/combat-resumption-state';
import { CurseApplicationState } from 'src/app/game-states/combat-field/curse-application-state';
import { DangerousDecisionMakingState } from 'src/app/game-states/combat-field/dangerous-decision-making-state';
import { EscapingState } from 'src/app/game-states/combat-field/escaping-state';
import { GameCompletion } from 'src/app/game-states/combat-field/game-completion';
import { RunAwayRollResolutionState } from 'src/app/game-states/combat-field/run-away-roll-resolution-state';
import { RunningAwayState } from 'src/app/game-states/combat-field/running-away-state';
import { WaitingState } from 'src/app/game-states/combat-field/waiting-state';
import { WinningState } from 'src/app/game-states/combat-field/winning-state';
import { Character } from 'src/app/models/character';
import { CombatField } from 'src/app/models/combat-field';
import { Equipment } from 'src/app/models/equipment';
import { Game } from 'src/app/models/game';
import { MunchkinCard } from 'src/app/models/munchkin-card';
import { Place } from 'src/app/models/place';
import { Player } from 'src/app/models/player';
import { CardService } from 'src/app/services/card.service';
import { GameService } from 'src/app/services/game.service';
import { SharedDataService } from 'src/app/services/shared-data.service';
import { SignalrService } from 'src/app/services/signalr.service';
import {
  RewardDialogComponent,
  RewardDialogData,
} from '../reward-dialog/reward-dialog.component';

@Component({
  selector: 'app-combat-field',
  templateUrl: './combat-field.component.html',
  styleUrls: ['./combat-field.component.css'],
})
export class CombatFieldComponent
  extends GameContext<CombatFieldComponent>
  implements OnInit, OnDestroy
{
  game!: Game;
  player!: Player;

  private subscriptions: Subscription[] = [];

  get place(): Place | undefined {
    return this.game.table.places.find((x) => x.player.id === this.player.id);
  }

  get cursePlace(): MunchkinCard | undefined {
    return this.combatField?.cursePlace;
  }

  get characterSquad(): Character[] | undefined {
    return this.combatField?.characterSquad;
  }

  get monsterSquad(): MunchkinCard[] | undefined {
    return this.combatField?.monsterSquad;
  }

  get combatField(): CombatField | undefined {
    return this.game?.table.combatField;
  }

  get character(): Character {
    return (
      this.game?.table.places.find((x) => x.player.id == this.player?.id)
        ?.character ?? ({} as Character)
    );
  }

  @ViewChild(ActionControlAreaDirective, { static: true })
  actionControlAreaDirective!: ActionControlAreaDirective;

  private states!: Map<string, GameState<CombatFieldComponent>>;
  private eventHandlers = new Map<string, (...args: any[]) => Promise<void>>([
    ['MonsterCardDrewEvent', this.onMonsterCardDrewEvent],
    ['CurseCardDrewEvent', this.onCurseCardDrewEvent],
    ['MonsterEnhancerCardDrewEvent', this.onMonsterEnhancerCardDrewEvent],
    ['CharacterAppliedCurseEvent', this.onCharacterAppliedCurseEvent],
    ['CharacterWonCombatEvent', this.onCharacterWonCombatEvent],
    ['CombatCompletedEvent', this.onCombatCompletedEvent],
    ['CharacterAskedForHelpEvent', this.onCharacterAskedForHelpEvent],
    ['CharacterGotHelpEvent', this.onCharacterGotHelpEvent],
    ['HelpTimeExpiredEvent', this.onHelpTimeExpiredEvent],
    ['CharacterRanAwayEvent', this.onCharacterRanAwayEvent],
    ['PlayerRolledDieEvent', this.onPlayerRolledDieEvent],
    ['CharacterAppliedBadStuffEvent', this.onCharacterAppliedBadStuffEvent],
    ['CharacterEscapedEvent', this.onCharacterEscapedEvent],
    ['OneShotCardPlayedEvent', this.onOneShotCardPlayedEvent],
    ['PlayerWonGameEvent', this.onPlayerWonGameEvent],
    ['MonsterCardPlayedEvent', this.onMonsterCardPlayedEvent],
    ['CurseCardPlayedEvent', this.onCurseCardPlayedEvent],
    ['PlayerLootedRoomEvent', this.onPlayerLootedRoomEvent],
  ]);

  constructor(
    private route: ActivatedRoute,
    private snackBar: MatSnackBar,
    private gameService: GameService,
    private cardService: CardService,
    private signalrService: SignalrService,
    private dataService: SharedDataService,
    public dialog: MatDialog
  ) {
    super();
  }

  async ngOnInit(): Promise<void> {
    const gameId = this.route.snapshot.paramMap.get('game-id') ?? '';
    const game = await this.dataService.getGame(gameId);
    this.subscriptions.push(game.subscribe((x: Game) => (this.game = x)));

    this.initCombatFieldStates();
    const state = this.states.get(this.game.state) ?? new WaitingState();
    this.transitionTo(state);

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

  async transitionTo(state: GameState<CombatFieldComponent>): Promise<void> {
    const succeeded = await this.gameService.updateGameState(
      this.game.id,
      state.constructor.name
    );

    if (succeeded) {
      this.state = state;
      this.state.setContext(this);
      this.state.occurs();
    }
  }

  async onMonsterCardDrewEvent(): Promise<void> {
    await this.transitionTo(new CombatInitiationState());
  }

  async onCurseCardDrewEvent(): Promise<void> {
    await this.transitionTo(new CurseApplicationState());
  }

  async onMonsterEnhancerCardDrewEvent(): Promise<void> {
    await this.transitionTo(new DangerousDecisionMakingState());
  }

  async onCharacterAppliedCurseEvent(): Promise<void> {
    await this.transitionTo(new WaitingState());
  }

  async onCharacterWonCombatEvent(): Promise<void> {
    await this.transitionTo(new WinningState(this.snackBar));
  }

  async onCombatCompletedEvent(): Promise<void> {
    await this.transitionTo(new CombatCompletionState());
  }

  async onCharacterAskedForHelpEvent(): Promise<void> {
    await this.transitionTo(new AskingForHelpState());
  }

  async onCharacterGotHelpEvent(): Promise<void> {
    await this.transitionTo(new CombatResumptionState());
  }

  async onHelpTimeExpiredEvent(): Promise<void> {
    await this.transitionTo(new CombatResumptionState());
  }

  async onCharacterRanAwayEvent(): Promise<void> {
    await this.transitionTo(new RunningAwayState());
  }

  async onPlayerRolledDieEvent(): Promise<void> {
    this.game = await this.gameService.getGame(this.game.id);
    await this.transitionTo(new RunAwayRollResolutionState());
  }

  async onCharacterAppliedBadStuffEvent(): Promise<void> {
    await this.transitionTo(new BadStuffApplicationState(this.snackBar));
  }

  async onOneShotCardPlayedEvent(): Promise<void> {
    if (this.game.state === 'RunAwayRollResolutionState') {
      await this.transitionTo(new RunAwayRollResolutionState());
    }
  }

  async onCharacterEscapedEvent(): Promise<void> {
    await this.transitionTo(new EscapingState(this.snackBar));
  }

  async onPlayerWonGameEvent(): Promise<void> {
    await this.transitionTo(new GameCompletion(this.dialog));
  }

  async onMonsterCardPlayedEvent(): Promise<void> {
    await this.transitionTo(new CombatInitiationState());
  }

  async onCurseCardPlayedEvent(): Promise<void> {}

  async onPlayerLootedRoomEvent(): Promise<void> {
    await this.transitionTo(new WaitingState());
  }

  applyCurse = async (): Promise<void> => {
    await this.gameService.applyCurse(this.game.id, this.character.id);
  };

  initiateCombat = async (): Promise<void> => {
    await this.gameService.initiateCombat(this.game.id, this.character.id);
  };

  comeToHelp = async (): Promise<void> => {
    await this.gameService.comeToHelp(this.game.id, this.character.id);
  };

  stopAskingForHelp = async (): Promise<void> => {
    await this.gameService.stopAskingForHelp(this.game.id);
  };

  resumeCombat = async (): Promise<void> => {
    await this.gameService.resumeCombat(this.game.id, this.character.id);
  };

  completeCombat = async (): Promise<void> => {
    await this.gameService.completeCombat(this.game.id);
  };

  rollDie = async (): Promise<void> => {
    await this.gameService.rollDie(this.game.id, this.player.id);
  };

  resolveRunAwayRoll = async (): Promise<void> => {
    await this.gameService.resolveRunAwayRoll(this.game.id, this.character.id);
  };

  isCurrentPlayerTurn(): boolean {
    return this.isPlayerTurn(this.player);
  }

  isPlayerTurn(player: Player): boolean {
    return this.gameService.isPlayerTurn(this.game, player);
  }

  showRewardStepper = async (): Promise<void> => {
    if (this.place === undefined) return;
    const itemCards = this.place.inHandCards.filter((x) =>
      this.cardService.isItemCard(x)
    );

    const cardsForPlay = this.place.inHandCards.filter(
      (x) => this.cardService.isItemCard(x) === false
    );

    const numberOfTreasures = this.game.table.combatField.monsterSquad
      .map((x) => x.treasures)
      .reduce((result, value) => result! + value!);

    const data = {
      itemCards,
      cardsForPlay,
      numberOfTreasures,
      selectedItemCards: [] as MunchkinCard[],
      selectedCardsForPlay: [] as MunchkinCard[],
    } as RewardDialogData;

    const dialogRef = this.dialog.open(RewardDialogComponent, { data });
    const result: RewardDialogData = await lastValueFrom(
      dialogRef.afterClosed()
    );
    if (result === undefined) return;

    await this.gameService.offerReward(
      this.game.id,
      this.player.id,
      result.selectedItemCards.map((x) => x.id),
      result.selectedCardsForPlay.map((x) => x.id),
      result.selectedNumberOfTreasures,
      result.helperPicksFirst
    );
  };

  initCombatFieldStates(): void {
    this.states = new Map<string, GameState<CombatFieldComponent>>([
      [AskingForHelpState.name, new AskingForHelpState()],
      [
        BadStuffApplicationState.name,
        new BadStuffApplicationState(this.snackBar),
      ],
      [CombatCompletionState.name, new CombatCompletionState()],
      [CombatInitiationState.name, new CombatInitiationState()],
      [CombatResumptionState.name, new CombatResumptionState()],
      [CurseApplicationState.name, new CurseApplicationState()],
      [EscapingState.name, new EscapingState(this.snackBar)],
      [RunAwayRollResolutionState.name, new RunAwayRollResolutionState()],
      [RunningAwayState.name, new RunningAwayState()],
      [WaitingState.name, new WaitingState()],
      [WinningState.name, new WinningState(this.snackBar)],
    ]);
  }

  getPlayerNicknameAbbreviation(character: Character): string {
    const place = this.game.table.places.find(
      (x) => x.character.id === character.id
    );
    return place ? this.cardService.getNicknameAbbreviation(place.player) : '';
  }

  getCharacterEquipmentDescription(equipment: Equipment): string {
    return this.cardService.getCharacterEquipmentDescription(equipment);
  }

  getCardDescription(card: MunchkinCard): string {
    return this.cardService.getCardDescription(card);
  }

  getCardImageSrc(card: MunchkinCard): string {
    return this.cardService.getCardImageSrc(card);
  }

  getMonsterEnhancers(card: MunchkinCard): string {
    let bonus = 0;
    if (this.combatField) {
      const enhancers = new Map<string, MunchkinCard[]>(
        Object.entries(this.combatField.monsterEnhancers)
      );
      bonus =
        enhancers
          ?.get(card.id)
          ?.map((x) => x.bonus ?? 0)
          .reduce((amount, current) => amount + current, 0) ?? 0;
    }
    return bonus > 0 ? '+' + bonus.toString() : bonus.toString();
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((x) => x.unsubscribe());
  }
}
