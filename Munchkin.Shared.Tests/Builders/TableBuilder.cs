using AutoFixture;
using Moq;
using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Cards.Base.Treasures.Items;
using Munchkin.Shared.Models;
using Munchkin.Shared.Offers;

namespace Munchkin.Shared.Tests.Builders
{
    public class TableBuilder
    {
        private readonly Table table = new(new Stack<DoorCard>(), new Stack<TreasureCard>());
        private readonly Fixture fixture = new();

        public TableBuilder()
        {
            fixture.Register(() => Mock.Of<MunchkinCard>());
            fixture.Register(() => Mock.Of<DoorCard>());
            fixture.Register(() => Mock.Of<TreasureCard>());
            fixture.Register(() => Mock.Of<CurseCard>());
            fixture.Register(() => Mock.Of<HeadgearCard>());
            fixture.Register(() => Mock.Of<ArmorCard>());
            fixture.Register(() => Mock.Of<FootgearCard>());
            fixture.Register(() => Mock.Of<HandCard>());

            fixture.Register(() => Mock.Of<Offer>());
        }

        public TableBuilder WithPlaces(params Place[] places)
        {
            table.Places = new(places);
            return this;
        }

        public TableBuilder WithPlaces(int count)
        {
            table.Places = new(fixture.CreateMany<Place>(count));
            return this;
        }

        public TableBuilder WithPlaces(Func<Fixture, IEnumerable<Place>> factory)
        {
            table.Places = new(factory(fixture));
            return this;
        }

        public TableBuilder WithCombatField(CombatField combatField)
        {
            table.CombatField = combatField;
            return this;
        }

        public TableBuilder WithDoorDeck(params DoorCard[] cards)
        {
            table.DoorDeck = new(cards);
            return this;
        }

        public TableBuilder WithDoorDeck(int count)
        {
            table.DoorDeck = new(fixture.CreateMany<DoorCard>(count));
            return this;
        }

        public TableBuilder WithTreasureDeck(int count)
        {
            table.TreasureDeck = new(fixture.CreateMany<TreasureCard>(count));
            return this;
        }

        public TableBuilder WithOffers(params Offer[] offers)
        {
            table.Offers = new(offers);
            return this;
        }

        public Table Build() => table;
    }
}
