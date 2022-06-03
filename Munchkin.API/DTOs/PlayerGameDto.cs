using Munchkin.Shared.Models;

namespace Munchkin.API.DTOs
{
    public class PlayerGameDto
    {
        public Guid Id { get; set; }
        public GameStatus Status { get; set; }
        public List<PlayerDto> Lobby { get; set; } = new();
        public List<PlayerDto> Table { get; set; } = new();
    }
}
