namespace Munchkin.API.DTOs
{
    public class CharacterDto
    {
        public Guid Id { get; set; }
        public int Level { get; set; }
        public PlayerDto Player { get; set; } = new();
    }
}
