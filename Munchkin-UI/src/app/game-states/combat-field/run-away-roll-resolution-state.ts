import { CombatFieldComponent } from 'src/app/components/combat-field/combat-field.component';
import { GameState } from '../base/game-state';

export class RunAwayRollResolutionState extends GameState<CombatFieldComponent> {
  constructor() {
    super();
  }
  public occurs(): void {
    const container = this.context.actionControlAreaDirective.viewContainerRef;
    container.clear();

    const isCurrentPlayerTurn = this.context.isCurrentPlayerTurn();

    if (isCurrentPlayerTurn) {
      this.setActionButton(
        'Resolve roll',
        this.context.resolveRunAwayRoll,
        container
      );
    }

    this.showSixSidedDie(this.context.game.table.dieValue, container);
  }
}
