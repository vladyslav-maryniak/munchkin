﻿using Munchkin.Shared.Cards.Base.Treasures;
using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record PlayerSoldCardsEvent(Guid GameId, Guid PlayerId, IEnumerable<Guid> CardIds) : IGameEvent
    {
        public static int MinAmountOfGoldPiecesForSale => 1000;

        public void Apply(Game game)
        {
            var place = game.Table.Places.First(x => x.Player.Id == PlayerId);
            var cardsForSale = place.InHandCards
                .Where(x => CardIds.Contains(x.Id))
                .Cast<ISaleable>();

            var goldPieces = cardsForSale
                .Select(x => x.GoldPieces)
                .Aggregate((total, x) => total + x);

            int upLevels = goldPieces / MinAmountOfGoldPiecesForSale;
            if (upLevels > 0)
            {
                place.Character.Level += upLevels;
                place.InHandCards.RemoveAll(x => cardsForSale.Select(x => x.Id).Contains(x.Id));
            }
        }
    }
}
