import { Character } from './character';
import { MunchkinCard } from './munchkin-card';

export interface CombatField {
  monsterSquad: MunchkinCard[];
  characterSquad: Character[];
  cursePlace: MunchkinCard;
}
