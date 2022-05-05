using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Base.Treasures.Items
{
    public abstract class OneHandCard : HandCard
    {
        public override bool TryEquip(Character character)
        {
            if (character.Equipment.LeftHand == null)
            {
                character.Equipment.LeftHand = this;
                return true;
            }
            if (character.Equipment.RightHand == null)
            {
                character.Equipment.RightHand = this;
                return true;
            }
            return false;
        }
    }
}
