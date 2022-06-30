import { Character } from './character';
import { MunchkinCard } from './munchkin-card';
import { Offer } from './offer';

export interface CombatField {
  monsterSquad: MunchkinCard[];
  characterSquad: Character[];
  reward: Offer;
  cursePlace: MunchkinCard;
}
