export interface Trade {
  id: string;
  offerorId: string;
  offereeId: string;
  offerorItemCardIds: string[];
  offereeItemCardIds: string[];
}
