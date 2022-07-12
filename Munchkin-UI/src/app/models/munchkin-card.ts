export interface MunchkinCard {
  code: string;
  id: string;
  name: string;
  description: string;
  level: number | null;
  badStuff: string | null;
  victoryLevels: number | null;
  bonus: number | null;
  goldPieces: number | null;
  treasures: number | null;
  metadata: Map<string, string> | null;
}
