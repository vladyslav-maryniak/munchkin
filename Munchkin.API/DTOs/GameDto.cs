namespace Munchkin.API.DTOs
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public List<PlayerDto> Lobby { get; set; } = new();
        public TableDto Table { get; set; } = new();
    }
}
