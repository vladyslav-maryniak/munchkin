import { CombatFieldComponent } from 'src/app/components/combat-field/combat-field.component';
import { GameState } from '../base/game-state';

export class CombatResumptionState extends GameState<CombatFieldComponent> {
  public occurs(): void {
    const container = this.context.actionControlAreaDirective.viewContainerRef;
    container.clear();

    if (this.context.isCurrentPlayerTurn()) {
      this.setActionButton(
        'Resume combat',
        this.context.resumeCombat,
        container
      );
    }
  }
}
