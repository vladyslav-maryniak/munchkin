using Munchkin.Shared.Cards.Base;

namespace Munchkin.Shared.Models
{
    public class Place
    {
        public Player Player { get; set; } = new();
        public Character Character { get; set; } = new();
        public List<MunchkinCard> InHandCards { get; set; } = new();

        public Place()
        {
        }

        public Place(Player player, Character character)
        {
            Player = player;
            Character = character;
        }
    }
}
