import { GameState } from './game-state';

export abstract class GameContext<T> {
  protected state!: GameState<T>;
  abstract transitionTo(state: GameState<T>): Promise<void>;
}
