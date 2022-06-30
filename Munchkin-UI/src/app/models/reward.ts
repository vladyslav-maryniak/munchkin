export interface Reward {
  id: string;
  offerorId: string;
  offereeId: string;
  itemCardIds: string[];
  cardIdsForPlay: string[];
  victoryTreasures: number;
  numberOfTreasures: number;
  helperPicksFirst: boolean;
}
