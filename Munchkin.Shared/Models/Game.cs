namespace Munchkin.Shared.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public int TurnIndex { get; set; }
        public string? State { get; set; }
        public GameStatus Status { get; set; }
        public GameLobby Lobby { get; set; } = new();
        public Table Table { get; set; }

        public Game(Table table)
        {
            Table = table;
        }

        public bool IsPlayerTurn(Guid playerId)
            => Table.Places[TurnIndex % Table.Places.Count].Player.Id == playerId;
    }
}
