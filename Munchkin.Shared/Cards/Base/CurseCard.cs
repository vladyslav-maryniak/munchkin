using Munchkin.Shared.Projections;

namespace Munchkin.Shared.Cards.Base
{
    public abstract class CurseCard : DoorCard
    {
        public abstract void Apply(Character character);
    }
}
