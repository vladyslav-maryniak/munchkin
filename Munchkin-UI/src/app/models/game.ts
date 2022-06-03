import { GameLobby } from './game-lobby';
import { GameStatus } from './game-status';
import { GameTable as GameTable } from './game-table';

export interface Game {
  id: string;
  turnIndex: number;
  state: string;
  status: GameStatus;
  lobby: GameLobby;
  table: GameTable;
}
