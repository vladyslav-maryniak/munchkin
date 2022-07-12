using AutoFixture;
using AutoFixture.Xunit2;
using Moq;
using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Cards.Base.Treasures;
using Munchkin.Shared.Cards.Base.Treasures.Items;
using Munchkin.Shared.Offers;

namespace Munchkin.Shared.Tests.Attributes
{
    public class AutoGameDataAttribute : AutoDataAttribute
    {
        public AutoGameDataAttribute()
            : base(() =>
            {
                var fixture = new Fixture();

                fixture.Register(() => Mock.Of<HeadgearCard>());
                fixture.Register(() => Mock.Of<ArmorCard>());
                fixture.Register(() => Mock.Of<FootgearCard>());
                fixture.Register(() => Mock.Of<HandCard>());

                fixture.Register(() => Mock.Of<MunchkinCard>());
                fixture.Register(() => Mock.Of<CurseCard>());
                fixture.Register(() => Mock.Of<MonsterCard>());
                fixture.Register(() => Mock.Of<GoUpLevelCard>());
                fixture.Register(() => Mock.Of<ItemCard>());
                fixture.Register(() => Mock.Of<OneShotCard>());
                fixture.Register(() => Mock.Of<MonsterEnhancerCard>());

                fixture.Register(() => Mock.Of<Offer>());

                return fixture;
            })
        {
        }
    }
}
