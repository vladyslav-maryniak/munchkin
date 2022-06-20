using Munchkin.Application.DbContext.MongoDb.Base;
using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Cards.Base.Treasures;
using Munchkin.Shared.Cards.Base.Treasures.Items;
using Munchkin.Shared.Cards.Doors.Curses;
using Munchkin.Shared.Cards.Doors.Monsters;
using Munchkin.Shared.Cards.Treasures.GoUpLevels;
using Munchkin.Shared.Cards.Treasures.Items.Armor;
using Munchkin.Shared.Cards.Treasures.Items.Footgear;
using Munchkin.Shared.Cards.Treasures.Items.Headgear;
using Munchkin.Shared.Cards.Treasures.Items.OneHand;
using Munchkin.Shared.Cards.Treasures.Items.TwoHands;
using Munchkin.Shared.Cards.Treasures.OneShots;
using Munchkin.Shared.Offers;

namespace Munchkin.Application.DbContext.MongoDb.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyCardMapConfiguration(this MapRegistrar builder)
        {
            MapRegistrar.ApplyConfiguration<DoorCard>();
            MapRegistrar.ApplyConfiguration<TreasureCard>();
            MapRegistrar.ApplyConfiguration<CurseCard>();
            MapRegistrar.ApplyConfiguration<MonsterCard>();
            MapRegistrar.ApplyConfiguration<GoUpLevelCard>();
            MapRegistrar.ApplyConfiguration<MunchkinCard>();
            MapRegistrar.ApplyConfiguration<OneShotCard>();
            MapRegistrar.ApplyConfiguration<ArmorCard>();
            MapRegistrar.ApplyConfiguration<FootgearCard>();
            MapRegistrar.ApplyConfiguration<HandCard>();
            MapRegistrar.ApplyConfiguration<HeadgearCard>();
            MapRegistrar.ApplyConfiguration<OneHandCard>();
            MapRegistrar.ApplyConfiguration<TwoHandsCard>();
            MapRegistrar.ApplyConfiguration<DuckOfDoom>();
            MapRegistrar.ApplyConfiguration<FlyingFrogs>();
            MapRegistrar.ApplyConfiguration<MaulRat>();
            MapRegistrar.ApplyConfiguration<UndeadHorse>();
            MapRegistrar.ApplyConfiguration<BoilAnAnthill>();
            MapRegistrar.ApplyConfiguration<ConvenientAdditionError>();
            MapRegistrar.ApplyConfiguration<InvokeObscureRules>();
            MapRegistrar.ApplyConfiguration<FlamingArmor>();
            MapRegistrar.ApplyConfiguration<BootsOfButtKicking>();
            MapRegistrar.ApplyConfiguration<HelmOfCourage>();
            MapRegistrar.ApplyConfiguration<SneakyBastardSword>();
            MapRegistrar.ApplyConfiguration<ElevenFootPole>();
            MapRegistrar.ApplyConfiguration<Doppleganger>();
            MapRegistrar.ApplyConfiguration<LoadedDie>();
        }

        public static void ApplyOfferMapConfiguration(this MapRegistrar builder)
        {
            MapRegistrar.ApplyConfiguration<Bribe>();
            MapRegistrar.ApplyConfiguration<Trade>();
            MapRegistrar.ApplyConfiguration<Reward>();
        }
    }
}
