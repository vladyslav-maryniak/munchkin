namespace Munchkin.Infrastucture.Projections
{
    public class Game
    {
        public Guid Id { get; set; }
        public List<Player> Players { get; set; } = new();

        public Game(Guid id)
        {
            Id = id;
        }
    }
}
