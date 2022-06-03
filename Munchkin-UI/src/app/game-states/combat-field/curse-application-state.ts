import { CombatFieldComponent } from 'src/app/components/combat-field/combat-field.component';
import { GameState } from '../base/game-state';

export class CurseApplicationState extends GameState<CombatFieldComponent> {
  public occurs(): void {
    const container = this.context.actionControlAreaDirective.viewContainerRef;
    container.clear();

    const isCurrentPlayerTurn = this.context.isCurrentPlayerTurn();

    if (isCurrentPlayerTurn) {
      this.setActionButton('Apply', this.context.applyCurse, container);
    }
  }
}
