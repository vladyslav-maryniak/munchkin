using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Base.Doors
{
    public abstract class CurseCard : DoorCard
    {
        public abstract void Apply(Character character);
    }
}
