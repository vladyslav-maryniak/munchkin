import { CombatField } from './combat-field';
import { Offer } from './offer';
import { Place } from './place';

export interface GameTable {
  places: Place[];
  offers: Offer[];
  combatField: CombatField;
  dieValue: number;
}
