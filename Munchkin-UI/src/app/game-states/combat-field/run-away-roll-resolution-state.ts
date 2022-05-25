import { MatSnackBar } from '@angular/material/snack-bar';
import { CombatFieldComponent } from 'src/app/components/combat-field/combat-field.component';
import { GameComponent } from 'src/app/pages/game/game.component';
import { GameState } from '../base/game-state';

export class RunAwayRollResolutionState extends GameState<CombatFieldComponent> {
  constructor(private dieValue: number, private snackBar: MatSnackBar) {
    super();
  }
  public occurs(): void {
    const container = this.context.actionControlAreaDirective.viewContainerRef;
    container.clear();

    const isCurrentPlayerTurn = this.context.isCurrentPlayerTurn();

    this.showSnackBar(
      `${this.dieValue} on top of the die.`,
      `${this.dieValue} on top of the die.`,
      isCurrentPlayerTurn,
      this.snackBar
    );

    if (isCurrentPlayerTurn) {
      this.setActionButton(
        'Resolve roll',
        this.context.resolveRunAwayRoll,
        container
      );
    }
  }
}
