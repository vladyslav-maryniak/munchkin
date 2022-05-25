import { MatSnackBar } from '@angular/material/snack-bar';
import { CombatFieldComponent } from 'src/app/components/combat-field/combat-field.component';
import { GameState } from '../base/game-state';

export class EscapingState extends GameState<CombatFieldComponent> {
  constructor(private snackBar: MatSnackBar) {
    super();
  }

  public async occurs(): Promise<void> {
    const container = this.context.actionControlAreaDirective.viewContainerRef;
    container.clear();

    const isCurrentPlayerTurn = this.context.isCurrentPlayerTurn();

    this.showSnackBar(
      "You'he successfully escaped from the monster.",
      "Player's successfully escaped from the monster.",
      isCurrentPlayerTurn,
      this.snackBar
    );

    if (isCurrentPlayerTurn) {
      await this.context.completeCombat();
    }
  }
}
