using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Doors.Monsters
{
    public class FaceSucker : MonsterCard
    {
        public override string Code => "face_sucker";
        public override int Level => 8;
        public override string Name => "Face Sucker";
        public override string Description => "It's gross! +6 against Elves.";
        public override string BadStuff => "When it sucks your face off, your Headgear goes with it. Discard the Headgear you are wearing, and lose a level.";
        public override int VictoryLevels => 1;
        public override int Treasures => 2;

        public override void ApplyBadStuff(Game game, Character character)
        {
            var equipment = character.Equipment;
            var item = equipment.TakeOff(equipment.Headgear);
            game.Table.Discard(item);

            character.Level--;
        }
    }
}
