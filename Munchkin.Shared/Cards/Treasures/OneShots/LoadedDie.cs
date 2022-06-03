using Munchkin.Shared.Cards.Base.Treasures;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Treasures.OneShots
{
    public class LoadedDie : OneShotCard
    {
        public override string Name => "Loaded Die";
        public override string Description => "Play after you roll the die, for any reason. Turn the die so the number of your choice is on top. That's your roll.";
        public override int GoldPieces => 300;
        public Dictionary<string, string> Metadata => new() { ["dieValue"] = string.Empty };

        public override bool TryUse(Table table, Dictionary<string, string> metadata)
        {
            if (metadata.TryGetValue("dieValue", out string? dieValueMeta)
                && int.TryParse(dieValueMeta, out int dieValue))
            {
                table.DieValue = dieValue;

                return true;
            }
            return false;
        }
    }
}
