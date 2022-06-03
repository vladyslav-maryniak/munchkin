import { CombatField } from './combat-field';
import { Place } from './place';

export interface GameTable {
  places: Place[];
  combatField: CombatField;
  dieValue: number;
}
