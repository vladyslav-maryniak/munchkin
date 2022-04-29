using Munchkin.Infrastucture.Projections;

namespace Munchkin.Infrastucture.Cards.Base
{
    public abstract class CurseCard : DoorCard
    {
        public abstract void Apply(Character character);
    }
}
