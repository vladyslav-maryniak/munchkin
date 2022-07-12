using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Base.Doors
{
    public abstract class CurseCard : DoorCard
    {
        public abstract void Apply(Game game, Character character);
        public virtual IDictionary<string, string> Metadata
            => new Dictionary<string, string>() { ["cursedCharacterId"] = string.Empty };
    }
}
