using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Cards.Base.Treasures;
using Munchkin.Shared.Cards.Base.Treasures.Items;

namespace Munchkin.Shared.Models
{
    public class Equipment
    {
        public HeadgearCard? Headgear { get; set; }
        public ArmorCard? Armor { get; set; }
        public FootgearCard? Footgear { get; set; }
        public HandCard? LeftHand { get; set; }
        public HandCard? RightHand { get; set; }

        public int Bonus
        {
            get
            {
                int bonus =
                    (Headgear?.Bonus ?? 0) +
                    (Armor?.Bonus ?? 0) +
                    (Footgear?.Bonus ?? 0) +
                    (LeftHand?.Bonus ?? 0);

                if (RightHand?.Id != LeftHand?.Id)
                {
                    bonus += RightHand?.Bonus ?? 0;
                }

                return bonus;
            }
        }

        public ItemCard TakeOff(Guid itemCardId)
        {
            ItemCard? takenOffItem = null;
            
            if (Headgear?.Id == itemCardId)
            {
                takenOffItem = Headgear;
                Headgear = null;
            }
            if (Armor?.Id == itemCardId)
            {
                takenOffItem = Armor;
                Armor = null;
            }
            if (Footgear?.Id == itemCardId)
            {
                takenOffItem = Footgear;
                Footgear = null;
            }
            if (LeftHand?.Id == itemCardId)
            {
                takenOffItem = LeftHand;
                LeftHand = null;
            }
            if (RightHand?.Id == itemCardId)
            {
                takenOffItem = RightHand;
                RightHand = null;
            }

            return takenOffItem!;
        }

        public IEnumerable<MunchkinCard> ToEnumerable()
        {
            List<MunchkinCard> equipment = new();

            if (Headgear is not null)
            {
                equipment.Add(Headgear);
            }
            if (Armor is not null)
            {
                equipment.Add(Armor);
            }
            if (Footgear is not null)
            {
                equipment.Add(Footgear);
            }
            if (LeftHand is not null)
            {
                equipment.Add(LeftHand);
            }
            if (RightHand is not null)
            {
                equipment.Add(RightHand);
            }

            return equipment;
        }
    }
}
