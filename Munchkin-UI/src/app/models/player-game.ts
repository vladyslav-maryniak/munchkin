import { GameStatus } from './game-status';
import { Player } from './player';

export interface PlayerGame {
  id: string;
  status: GameStatus;
  lobby: Player[];
  table: Player[];
}
