import { Equipment } from './equipment';

export interface Character {
  id: string;
  level: number;
  equipment: Equipment;
}
