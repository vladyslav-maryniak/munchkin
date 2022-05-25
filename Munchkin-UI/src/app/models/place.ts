import { Character } from './character';
import { MunchkinCard } from './munchkin-card';
import { Player } from './player';

export interface Place {
  player: Player;
  character: Character;
  inHandCards: MunchkinCard[];
}
