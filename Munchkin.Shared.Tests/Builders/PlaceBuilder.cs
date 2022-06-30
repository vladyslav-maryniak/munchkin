using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Tests.Builders
{
    public class PlaceBuilder
    {
        private readonly Place place = new();

        public PlaceBuilder WithPlayer(Player player)
        {
            place.Player = player;
            return this;
        }

        public PlaceBuilder WithCharacter(Character character)
        {
            place.Character = character;
            return this;
        }

        public PlaceBuilder WithInHandCards(params MunchkinCard[] cards)
        {
            place.InHandCards = new(cards);
            return this;
        }

        public Place Build() => place;
    }
}
