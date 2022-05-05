using Munchkin.Shared.Models;

namespace Munchkin.Shared.Cards.Base.Treasures.Items
{
    public abstract class TwoHandsCard : HandCard
    {
        public override bool TryEquip(Character character)
        {
            var equipment = character.Equipment;

            if (equipment.LeftHand is null && equipment.RightHand is null)
            {
                equipment.LeftHand = this;
                equipment.RightHand = this;
                return true;
            }
            return false;
        }
    }
}
