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
    }
}
