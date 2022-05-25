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

namespace Munchkin.Application.DbContext.MongoDb.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyCardMapConfiguration(this ModelBuilder builder)
        {
            builder.ApplyConfiguration<DoorCard>();
            builder.ApplyConfiguration<TreasureCard>();
            builder.ApplyConfiguration<CurseCard>();
            builder.ApplyConfiguration<MonsterCard>();
            builder.ApplyConfiguration<GoUpLevelCard>();
            builder.ApplyConfiguration<MunchkinCard>();
            builder.ApplyConfiguration<OneShotCard>();
            builder.ApplyConfiguration<ArmorCard>();
            builder.ApplyConfiguration<FootgearCard>();
            builder.ApplyConfiguration<HandCard>();
            builder.ApplyConfiguration<HeadgearCard>();
            builder.ApplyConfiguration<OneHandCard>();
            builder.ApplyConfiguration<TwoHandsCard>();
            builder.ApplyConfiguration<DuckOfDoom>();
            builder.ApplyConfiguration<FlyingFrogs>();
            builder.ApplyConfiguration<MaulRat>();
            builder.ApplyConfiguration<UndeadHorse>();
            builder.ApplyConfiguration<BoilAnAnthill>();
            builder.ApplyConfiguration<ConvenientAdditionError>();
            builder.ApplyConfiguration<InvokeObscureRules>();
            builder.ApplyConfiguration<FlamingArmor>();
            builder.ApplyConfiguration<BootsOfButtKicking>();
            builder.ApplyConfiguration<HelmOfCourage>();
            builder.ApplyConfiguration<SneakyBastardSword>();
            builder.ApplyConfiguration<ElevenFootPole>();
            builder.ApplyConfiguration<Doppleganger>();
            builder.ApplyConfiguration<LoadedDie>();
        }
    }
}
