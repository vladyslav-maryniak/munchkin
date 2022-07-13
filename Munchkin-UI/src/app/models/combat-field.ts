import { Character } from './character';
import { MunchkinCard } from './munchkin-card';
import { Offer } from './offer';

export interface CombatField {
  monsterSquad: MunchkinCard[];
  monsterEnhancers: Map<string, MunchkinCard[]>;
  characterSquad: Character[];
  reward: Offer;
  cursePlace: MunchkinCard;
}
