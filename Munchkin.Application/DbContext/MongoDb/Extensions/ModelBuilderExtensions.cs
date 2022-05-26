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
            ModelBuilder.ApplyConfiguration<DoorCard>();
            ModelBuilder.ApplyConfiguration<TreasureCard>();
            ModelBuilder.ApplyConfiguration<CurseCard>();
            ModelBuilder.ApplyConfiguration<MonsterCard>();
            ModelBuilder.ApplyConfiguration<GoUpLevelCard>();
            ModelBuilder.ApplyConfiguration<MunchkinCard>();
            ModelBuilder.ApplyConfiguration<OneShotCard>();
            ModelBuilder.ApplyConfiguration<ArmorCard>();
            ModelBuilder.ApplyConfiguration<FootgearCard>();
            ModelBuilder.ApplyConfiguration<HandCard>();
            ModelBuilder.ApplyConfiguration<HeadgearCard>();
            ModelBuilder.ApplyConfiguration<OneHandCard>();
            ModelBuilder.ApplyConfiguration<TwoHandsCard>();
            ModelBuilder.ApplyConfiguration<DuckOfDoom>();
            ModelBuilder.ApplyConfiguration<FlyingFrogs>();
            ModelBuilder.ApplyConfiguration<MaulRat>();
            ModelBuilder.ApplyConfiguration<UndeadHorse>();
            ModelBuilder.ApplyConfiguration<BoilAnAnthill>();
            ModelBuilder.ApplyConfiguration<ConvenientAdditionError>();
            ModelBuilder.ApplyConfiguration<InvokeObscureRules>();
            ModelBuilder.ApplyConfiguration<FlamingArmor>();
            ModelBuilder.ApplyConfiguration<BootsOfButtKicking>();
            ModelBuilder.ApplyConfiguration<HelmOfCourage>();
            ModelBuilder.ApplyConfiguration<SneakyBastardSword>();
            ModelBuilder.ApplyConfiguration<ElevenFootPole>();
            ModelBuilder.ApplyConfiguration<Doppleganger>();
            ModelBuilder.ApplyConfiguration<LoadedDie>();
        }
    }
}
