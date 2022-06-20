export interface Offer {
  id: string;
  offerorId: string;
  offereeId: string;
  agreement: string;
  itemCardIds: string[];
  itemCardIdsForPlay: string[];
  victoryTreasures: number;
  numberOfTreasures: number;
  helperPicksFirst: boolean;
  offerorItemCardIds: string[];
  offereeItemCardIds: string[];
}
