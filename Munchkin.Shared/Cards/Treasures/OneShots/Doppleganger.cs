using Munchkin.Shared.Cards.Base.Treasures;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Treasures.OneShots
{
    public class Doppleganger : OneShotCard
    {
        public override string Name => "Doppleganger";
        public override string Description => "Summons your exact duplicate, who fights beside you, so you double your combat strength. You may use the Doppleganger only when you are the only player in the combat. Usable once only.";
        public override int GoldPieces => 300;

        public override bool TryUse(Table table, Dictionary<string, string> metadata)
        {
            var squad = table.CombatField.CharacterSquad;
            
            if (squad.Count == 1)
            {
                var character = squad.First();
                squad.Add(character);
                return true;
            }
            return false;
        }
    }
}
