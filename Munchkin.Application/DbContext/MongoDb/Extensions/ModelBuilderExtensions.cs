using Munchkin.Application.DbContext.MongoDb.Base;
using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Cards.Base.Treasures;
using Munchkin.Shared.Cards.Base.Treasures.Items;
using Munchkin.Shared.Cards.Doors.Curses;
using Munchkin.Shared.Cards.Doors.MonsterEnhancers;
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
            MapRegistrar.ApplyConfiguration<LoseArmor>();
            MapRegistrar.ApplyConfiguration<LoseFootgear>();
            MapRegistrar.ApplyConfiguration<LoseHeadgear>();
            MapRegistrar.ApplyConfiguration<LoseLevelElf>();
            MapRegistrar.ApplyConfiguration<LoseLevelLightning>();
            MapRegistrar.ApplyConfiguration<TrulyObnoxiousCurse>();
            MapRegistrar.ApplyConfiguration<Ancient>();
            MapRegistrar.ApplyConfiguration<Baby>();
            MapRegistrar.ApplyConfiguration<Enraged>();
            MapRegistrar.ApplyConfiguration<Humongous>();
            MapRegistrar.ApplyConfiguration<Intelligent>();
            MapRegistrar.ApplyConfiguration<Bigfoot>();
            MapRegistrar.ApplyConfiguration<Crabs>();
            MapRegistrar.ApplyConfiguration<DroolingSlime>();
            MapRegistrar.ApplyConfiguration<FaceSucker>();
            MapRegistrar.ApplyConfiguration<FlyingFrogs>();
            MapRegistrar.ApplyConfiguration<Harpies>();
            MapRegistrar.ApplyConfiguration<LameGoblin>();
            MapRegistrar.ApplyConfiguration<MaulRat>();
            MapRegistrar.ApplyConfiguration<MrBones>();
            MapRegistrar.ApplyConfiguration<PottedPlant>();
            MapRegistrar.ApplyConfiguration<UndeadHorse>();
            MapRegistrar.ApplyConfiguration<BoilAnAnthill>();
            MapRegistrar.ApplyConfiguration<BribeGmWithFood>();
            MapRegistrar.ApplyConfiguration<ConvenientAdditionError>();
            MapRegistrar.ApplyConfiguration<InvokeObscureRules>();
            MapRegistrar.ApplyConfiguration<OneHundredGoldPieces>();
            MapRegistrar.ApplyConfiguration<PotionOfGeneralStudliness>();
            MapRegistrar.ApplyConfiguration<FlamingArmor>();
            MapRegistrar.ApplyConfiguration<LeatherArmor>();
            MapRegistrar.ApplyConfiguration<SlimyArmor>();
            MapRegistrar.ApplyConfiguration<BootsOfButtKicking>();
            MapRegistrar.ApplyConfiguration<BadAssBandana>();
            MapRegistrar.ApplyConfiguration<HelmOfCourage>();
            MapRegistrar.ApplyConfiguration<HornyHelmet>();
            MapRegistrar.ApplyConfiguration<BucklerOfSwashing>();
            MapRegistrar.ApplyConfiguration<CheeseGraterOfPeace>();
            MapRegistrar.ApplyConfiguration<SneakyBastardSword>();
            MapRegistrar.ApplyConfiguration<ChainsawOfBloodyDismemberment>();
            MapRegistrar.ApplyConfiguration<ElevenFootPole>();
            MapRegistrar.ApplyConfiguration<SwissArmyPolearm>();
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
