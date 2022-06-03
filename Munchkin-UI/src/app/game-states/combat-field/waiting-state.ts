import { CombatFieldComponent } from 'src/app/components/combat-field/combat-field.component';
import { GameState } from '../base/game-state';

export class WaitingState extends GameState<CombatFieldComponent> {
  public occurs(): void {
    const viewContainerRef =
      this.context.actionControlAreaDirective.viewContainerRef;
    viewContainerRef.clear();
  }
}
