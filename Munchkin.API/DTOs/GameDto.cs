using Munchkin.Shared.Models;

namespace Munchkin.API.DTOs
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public int TurnIndex { get; set; }
        public string? State { get; set; }
        public GameStatus Status { get; set; }
        public GameLobbyDto Lobby { get; set; } = new();
        public TableDto Table { get; set; } = new();
    }
}
