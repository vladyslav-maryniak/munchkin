namespace Munchkin.API.DTOs
{
    public class LeavePlayerDto
    {
        public Guid GameId { get; set; }
        public PlayerDto Player { get; set; } = new();
    }
}
