import { GameLobby } from './game-lobby';

export interface Game {
  id: string;
  lobby: GameLobby;
}
