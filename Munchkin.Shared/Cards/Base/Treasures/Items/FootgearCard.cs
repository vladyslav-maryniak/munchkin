using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Base.Treasures.Items
{
    public abstract class FootgearCard : ItemCard
    {
        public override bool TryEquip(Character character)
        {
            if (character.Equipment.Footgear is null)
            {
                character.Equipment.Footgear = this;
                return true;
            }
            return false;
        }
    }
}
