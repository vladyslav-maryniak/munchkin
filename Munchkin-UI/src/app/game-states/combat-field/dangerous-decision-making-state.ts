import { CombatFieldComponent } from 'src/app/components/combat-field/combat-field.component';
import { GameState } from '../base/game-state';

export class DangerousDecisionMakingState extends GameState<CombatFieldComponent> {
  public occurs(): void {}
}
