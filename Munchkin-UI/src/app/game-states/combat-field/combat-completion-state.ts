import { CombatFieldComponent } from 'src/app/components/combat-field/combat-field.component';
import { GameState } from '../base/game-state';
import { WaitingState } from './waiting-state';

export class CombatCompletionState extends GameState<CombatFieldComponent> {
  public occurs(): void {
    this.context.transitionTo(new WaitingState());
  }
}
