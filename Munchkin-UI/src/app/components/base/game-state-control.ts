export interface GameStateControl {
  data: any;
  action(): Promise<void>;
}
