namespace Munchkin.API.DTOs
{
    public class JoinPlayerDto
    {
        public Guid GameId { get; set; }
        public PlayerDto Player { get; set; } = new();
    }
}
