import { CombatFieldComponent } from 'src/app/components/combat-field/combat-field.component';
import { GameState } from '../base/game-state';

export class RunningAwayState extends GameState<CombatFieldComponent> {
  public occurs(): void {
    const container = this.context.actionControlAreaDirective.viewContainerRef;
    container.clear();

    if (this.context.isCurrentPlayerTurn()) {
      this.setActionButton('Roll die', this.context.rollDie, container);
    }
  }
}
