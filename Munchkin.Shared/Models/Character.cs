using Munchkin.Shared.Cards.Base.Doors;

namespace Munchkin.Shared.Models
{
    public class Character
    {
        public Guid Id { get; set; }
        public int Level { get; set; } = 1;
        public Equipment Equipment { get; set; } = new();
        public List<CurseCard> Curses { get; set; } = new();

        public Character()
        {
            Id = Guid.NewGuid();
        }
    }
}
