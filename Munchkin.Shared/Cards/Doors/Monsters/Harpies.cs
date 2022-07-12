using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Doors.Monsters
{
    public class Harpies : MonsterCard
    {
        public override string Code => "harpies";
        public override int Level => 4;
        public override string Name => "Harpies";
        public override string Description => "They resist magic. +5 against Wizards.";
        public override string BadStuff => "Their music is really, really bad. Lose 2 levels.";
        public override int VictoryLevels => 1;
        public override int Treasures => 2;

        public override void ApplyBadStuff(Game game, Character character)
        {
            character.Level -= 2;
        }
    }
}
