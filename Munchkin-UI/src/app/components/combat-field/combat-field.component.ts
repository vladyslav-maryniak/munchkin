import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { ActionControlAreaDirective } from 'src/app/directives/action-control-area.directive';
import { GameContext } from 'src/app/game-states/base/game-context';
import { GameState } from 'src/app/game-states/base/game-state';
import { AskingForHelpState } from 'src/app/game-states/combat-field/asking-for-help-state';
import { BadStuffApplicationState } from 'src/app/game-states/combat-field/bad-stuff-application-state';
import { CombatCompletionState } from 'src/app/game-states/combat-field/combat-completion-state';
import { CombatInitiationState } from 'src/app/game-states/combat-field/combat-initiation-state';
import { CombatResumptionState } from 'src/app/game-states/combat-field/combat-resumption-state';
import { CurseApplicationState } from 'src/app/game-states/combat-field/curse-application-state';
import { EscapingState } from 'src/app/game-states/combat-field/escaping-state';
import { RunAwayRollResolutionState } from 'src/app/game-states/combat-field/run-away-roll-resolution-state';
import { RunningAwayState } from 'src/app/game-states/combat-field/running-away-state';
import { WaitingState } from 'src/app/game-states/combat-field/waiting-state';
import { WinningState } from 'src/app/game-states/combat-field/winning-state';
import { Character } from 'src/app/models/character';
import { CombatField } from 'src/app/models/combat-field';
import { Equipment } from 'src/app/models/equipment';
import { Game } from 'src/app/models/game';
import { ItemCard } from 'src/app/models/item-card';
import { Player } from 'src/app/models/player';
import { GameService } from 'src/app/services/game.service';
import { SignalrService } from 'src/app/services/signalr.service';

@Component({
  selector: 'app-combat-field',
  templateUrl: './combat-field.component.html',
  styleUrls: ['./combat-field.component.css'],
})
export class CombatFieldComponent
  extends GameContext<CombatFieldComponent>
  implements OnInit, OnDestroy
{
  @Input() game!: Game;
  @Input() player!: Player;
  character!: Character;

  get combatField(): CombatField {
    return this.game.table.combatField;
  }

  @ViewChild(ActionControlAreaDirective, { static: true })
  actionControlAreaDirective!: ActionControlAreaDirective;

  private subscription!: Subscription;

  private states!: Map<string, GameState<CombatFieldComponent>>;
  private eventHandlers = new Map<string, (...args: any[]) => Promise<void>>([
    ['ItemCardPlayedEvent', this.onItemCardPlayedEvent],
    ['OneShotCardPlayedEvent', this.onOneShotCardPlayedEvent],
    ['GoUpLevelCardPlayedEvent', this.onGoUpLevelCardPlayedEvent],
    ['MonsterCardDrewEvent', this.onMonsterCardDrewEvent],
    ['CurseCardDrewEvent', this.onCurseCardDrewEvent],
    ['CharacterWonCombatEvent', this.onCharacterWonCombatEvent],
    ['CombatCompletedEvent', this.onCombatCompletedEvent],
    ['CharacterAskedForHelpEvent', this.onCharacterAskedForHelpEvent],
    ['CharacterGotHelpEvent', this.onCharacterGotHelpEvent],
    ['HelpTimeExpiredEvent', this.onHelpTimeExpiredEvent],
    ['CharacterRanAwayEvent', this.onCharacterRanAwayEvent],
    ['PlayerRolledDieEvent', this.onPlayerRolledDieEvent],
    ['CharacterAppliedBadStuffEvent', this.onCharacterAppliedBadStuffEvent],
    ['CharacterEscapedEvent', this.onCharacterEscapedEvent],
  ]);

  constructor(
    private snackBar: MatSnackBar,
    private gameService: GameService,
    private signalrService: SignalrService
  ) {
    super();
  }

  async ngOnInit(): Promise<void> {
    this.character =
      this.game.table.places.find((x) => x.player.id == this.player.id)
        ?.character ?? ({} as Character);

    this.initCombatFieldStates();
    const state = this.states.get(this.game.state) ?? new WaitingState();
    this.transitionTo(state);

    this.subscription = this.signalrService.gameEvents.subscribe(this.onEvent);
  }

  private onEvent = async (event: string): Promise<void> => {
    if (this.eventHandlers.has(event)) {
      await this.eventHandlers.get(event)?.call(this);
      this.game = await this.gameService.getGame(this.game.id);
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

  async onItemCardPlayedEvent(): Promise<void> {}

  async onOneShotCardPlayedEvent(): Promise<void> {}

  async onGoUpLevelCardPlayedEvent(): Promise<void> {}

  async onMonsterCardDrewEvent(): Promise<void> {
    await this.transitionTo(new CombatInitiationState());
  }

  async onCurseCardDrewEvent(): Promise<void> {
    await this.transitionTo(new CurseApplicationState(this.snackBar));
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
    await this.transitionTo(
      new RunAwayRollResolutionState(this.game.table.dieValue, this.snackBar)
    );
  }

  async onCharacterAppliedBadStuffEvent(): Promise<void> {
    await this.transitionTo(new BadStuffApplicationState(this.snackBar));
  }

  async onCharacterEscapedEvent(): Promise<void> {
    await this.transitionTo(new EscapingState(this.snackBar));
  }

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
    const index = this.game.turnIndex % this.game.table.places.length;
    return this.game.table.places[index].player.id == player.id;
  }

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
      [CurseApplicationState.name, new CurseApplicationState(this.snackBar)],
      [EscapingState.name, new EscapingState(this.snackBar)],
      [
        RunAwayRollResolutionState.name,
        new RunAwayRollResolutionState(this.game.table.dieValue, this.snackBar),
      ],
      [RunningAwayState.name, new RunningAwayState()],
      [WaitingState.name, new WaitingState()],
      [WinningState.name, new WinningState(this.snackBar)],
    ]);
  }

  getNicknameLabel(character: Character): string {
    const place = this.game.table.places.find(
      (x) => x.character.id === character.id
    );
    return place?.player.nickname.trim().charAt(0).toUpperCase() ?? '';
  }

  getEquipmentDescription(e: Equipment): string {
    const total =
      (e.headgear?.bonus ?? 0) +
      (e.armor?.bonus ?? 0) +
      (e.footgear?.bonus ?? 0) +
      (e.leftHand?.bonus ?? 0) +
      (e.leftHand?.id != e.rightHand?.id ? e.rightHand?.bonus ?? 0 : 0);

    const description: string[] = [
      `Headgear: ${this.getItemDescription(e.headgear)}`,
      `Armor: ${this.getItemDescription(e.armor)}`,
      `Footgear: ${this.getItemDescription(e.footgear)}`,
      `Left hand: ${this.getItemDescription(e.leftHand)}`,
      `Right hand: ${this.getItemDescription(e.rightHand)}`,
      `Total: +${total}`,
    ];

    return description.join('\n');
  }

  private getItemDescription(card: ItemCard): string {
    return card ? `${card.name} +${card.bonus}` : 'none';
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
