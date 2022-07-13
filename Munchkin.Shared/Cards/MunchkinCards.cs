using Munchkin.Shared.Cards.Base;
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

namespace Munchkin.Shared.Cards
{
    public static class MunchkinCards
    {
        public static IEnumerable<DoorCard> DoorCards => new DoorCard[]
        {
            new DuckOfDoom(),
            new LoseArmor(),
            new LoseFootgear(),
            new LoseHeadgear(),
            new LoseLevelElf(),
            new LoseLevelLightning(),
            new TrulyObnoxiousCurse(),
            new Ancient(),
            new Baby(),
            new Enraged(),
            new Humongous(),
            new Intelligent(),
            new Bigfoot(),
            new Crabs(),
            new DroolingSlime(),
            new FaceSucker(),
            new FlyingFrogs(),
            new Harpies(),
            new LameGoblin(),
            new MaulRat(),
            new MrBones(),
            new PottedPlant(),
            new UndeadHorse()
        };

        public static IEnumerable<TreasureCard> TreasureCards => new TreasureCard[]
        {
            new BoilAnAnthill(),
            new BribeGmWithFood(),
            new ConvenientAdditionError(),
            new InvokeObscureRules(),
            new OneHundredGoldPieces(),
            new PotionOfGeneralStudliness(),
            new FlamingArmor(),
            new LeatherArmor(),
            new SlimyArmor(),
            new BootsOfButtKicking(),
            new BadAssBandana(),
            new HelmOfCourage(),
            new HornyHelmet(),
            new BucklerOfSwashing(),
            new CheeseGraterOfPeace(),
            new SneakyBastardSword(),
            new ChainsawOfBloodyDismemberment(),
            new ElevenFootPole(),
            new SwissArmyPolearm(),
            new Doppleganger(),
            new LoadedDie()
        };
    }
}
