import { CombatFieldComponent } from 'src/app/components/combat-field/combat-field.component';
import { GameState } from '../base/game-state';

export class AskingForHelpState extends GameState<CombatFieldComponent> {
  public occurs(): void {
    const container = this.context.actionControlAreaDirective.viewContainerRef;
    container.clear();

    if (this.context.isCurrentPlayerTurn()) {
      this.setActionButton(
        'Stop asking for help',
        this.context.stopAskingForHelp,
        container
      );
      this.setActionButton(
        'Offer reward',
        this.context.showRewardStepper,
        container
      );
    } else {
      this.setActionButton('Come to help', this.context.comeToHelp, container);
    }
  }
}
