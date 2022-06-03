import { Equipment } from './equipment';
import { MunchkinCard } from './munchkin-card';

export interface Character {
  id: string;
  level: number;
  equipment: Equipment;
  curses: MunchkinCard[];
}
