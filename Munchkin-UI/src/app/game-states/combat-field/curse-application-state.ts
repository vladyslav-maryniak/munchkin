import { MatSnackBar } from '@angular/material/snack-bar';
import { CombatFieldComponent } from 'src/app/components/combat-field/combat-field.component';
import { GameState } from '../base/game-state';
import { WaitingState } from './waiting-state';

export class CurseApplicationState extends GameState<CombatFieldComponent> {
  constructor(private snackBar: MatSnackBar) {
    super();
  }

  public occurs(): void {
    this.showSnackBar(
      "You'he applied a curse!",
      "Player's applied a curse!",
      this.context.isCurrentPlayerTurn(),
      this.snackBar
    );

    this.context.transitionTo(new WaitingState());
  }
}
