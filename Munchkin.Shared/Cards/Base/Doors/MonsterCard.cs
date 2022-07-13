using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Base.Doors
{
    public abstract class MonsterCard : DoorCard
    {
        public abstract int Level { get; }
        public abstract string BadStuff { get; }
        public abstract int VictoryLevels { get; }
        public abstract int Treasures { get; }
        public abstract void ApplyBadStuff(Game game, Character character);
    }
}
