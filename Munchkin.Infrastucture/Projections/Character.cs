namespace Munchkin.Infrastucture.Projections
{
    public class Character
    {
        public Guid Id { get; set; }
        public Player Player { get; set; }
        public int Level { get; set; } = 1;

        public Character(Player player)
        {
            Id = Guid.NewGuid();
            Player = player;
        }
    }
}
