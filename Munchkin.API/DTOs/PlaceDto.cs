namespace Munchkin.API.DTOs
{
    public class PlaceDto
    {
        public PlayerDto? Player { get; set; }
        public CharacterDto Character { get; set; } = new();
        public List<object> InHandCards { get; set; } = new();
    }
}
