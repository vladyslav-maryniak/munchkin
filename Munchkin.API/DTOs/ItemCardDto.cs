namespace Munchkin.API.DTOs
{
    public class ItemCardDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Bonus { get; set; }
        public int GoldPieces { get; set; }
    }
}
