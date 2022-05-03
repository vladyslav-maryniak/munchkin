namespace Munchkin.Infrastucture.Projections
{
    public class Character
    {
        public Guid Id { get; set; }
        public Player Player { get; set; }
        public int Level { get; set; }

        public Character(Player player)
        {
            Id = Guid.NewGuid();
            Player = player;
            Level = 1;
        }
    }
}
