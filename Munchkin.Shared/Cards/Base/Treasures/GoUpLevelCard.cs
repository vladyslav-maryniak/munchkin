using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Base.Treasures
{
    public abstract class GoUpLevelCard : TreasureCard
    {
        public virtual bool TryRiseLevel(Character character)
        {
            const int maxLevel = 9; // 10th level must be reached in combat
            if (character.Level < maxLevel)
            {
                character.Level++;
                return true;
            }
            return false;
        }
    }
}
