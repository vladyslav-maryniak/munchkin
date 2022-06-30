using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;
using Munchkin.Shared.Offers;

namespace Munchkin.Shared.Events
{
    public record PlayerOfferedRewardEvent(
        Guid GameId,
        Guid OfferorId,
        Guid[] ItemCardIds,
        Guid[] CardIdsForPlay,
        int VictoryTreasures,
        int NumberOfTreasures,
        bool HelperPicksFirst) : IGameEvent
    {
        public void Apply(Game game)
        {
            Reward offer = new(
                OfferorId,
                ItemCardIds,
                CardIdsForPlay,
                VictoryTreasures,
                NumberOfTreasures,
                HelperPicksFirst);

            game.Table.CombatField.Reward = offer;
        }
    }
}
