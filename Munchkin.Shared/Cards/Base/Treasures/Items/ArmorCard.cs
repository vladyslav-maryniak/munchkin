using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Base.Treasures.Items
{
    public abstract class ArmorCard : ItemCard
    {
        public override bool TryEquip(Character character)
        {
            if (character.Equipment.Armor is null)
            {
                character.Equipment.Armor = this;
                return true;
            }
            return false;
        }
    }
}
