namespace Munchkin.API.DTOs
{
    public class PlayerDto
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; } = string.Empty;
    }
}