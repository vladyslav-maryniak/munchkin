using Munchkin.Shared.Cards.Base;

namespace Munchkin.Shared.Models
{
    public class Place
    {
        public Player Player { get; set; }
        public Character Character { get; set; }
        public List<MunchkinCard> InHandCards { get; set; } = new();

        public Place(Player player, Character character)
        {
            Player = player;
            Character = character;
        }
    }
}
