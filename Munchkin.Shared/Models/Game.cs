namespace Munchkin.Shared.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public List<Player> Lobby { get; set; } = new();
        public Table Table { get; set; } = new();
    }
}
