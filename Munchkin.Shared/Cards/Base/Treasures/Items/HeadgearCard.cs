using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Base.Treasures.Items
{
    public abstract class HeadgearCard : ItemCard
    {
        public override bool TryEquip(Character character)
        {
            if (character.Equipment.Headgear is null)
            {
                character.Equipment.Headgear = this;
                return true;
            }
            return false;
        }
    }
}
