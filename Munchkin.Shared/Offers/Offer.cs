using Munchkin.Shared.Models;

namespace Munchkin.Shared.Offers
{
    public abstract class Offer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OfferorId { get; set; }
        public Guid OffereeId { get; set; }

        protected Offer()
        {
        }

        protected Offer(Guid offerorId, Guid offereeId)
        {
            OfferorId = offerorId;
            OffereeId = offereeId;
        }

        public abstract void Perform(Game game);

        protected virtual void TransferCards(
            Guid fromId, Guid toId, Guid[] cardIds, Game game)
        {
            var (From, To) = GetParties(fromId, toId, game);
            var transferItems = From.InHandCards.Where(x => cardIds.Contains(x.Id));
            From.InHandCards = From.InHandCards.Except(transferItems).ToList();
            To.InHandCards.AddRange(transferItems);
        }

        protected virtual void TransferEquipment(
            Guid fromId, Guid toId, Guid[] itemCardIds, Game game)
        {
            var (From, To) = GetParties(fromId, toId, game);
            foreach (var itemCardId in itemCardIds)
            {
                var takenOffItem = From.Character.Equipment.TakeOff(itemCardId);
                To.InHandCards.Add(takenOffItem);
            }
        }

        protected virtual void TransferTreasures(
            Guid fromId, Guid toId, int victoryTreasures, int numberOfTreasures, bool helperPicksFirst, Game game)
        {
            var (From, To) = GetParties(fromId, toId, game);
            
            var treasureCards = From.InHandCards.TakeLast(victoryTreasures);
            var helperCards = helperPicksFirst ?
                treasureCards.Take(numberOfTreasures) :
                treasureCards.TakeLast(numberOfTreasures);

            To.InHandCards.AddRange(helperCards);
            From.InHandCards.RemoveAll(x => helperCards.Contains(x));
        }

        private static (Place From, Place To) GetParties(Guid fromId, Guid toId, Game game)
        {
            var from = game.Table.Places.First(x => x.Player.Id == fromId);
            var to = game.Table.Places.First(x => x.Player.Id == toId);
            return (from, to);
        }
    }
}
