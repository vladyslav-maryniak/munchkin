namespace Munchkin.Shared.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public int TurnIndex { get; set; }
        public List<Player> Lobby { get; set; } = new();
        public Table Table { get; set; } = new();

        public bool IsPlayerTurn(Guid playerId)
            => Table.Places[TurnIndex].Player.Id == playerId;
    }
}
