namespace Munchkin.API.DTOs
{
    public class TableDto
    {
        public List<MonsterCardDto> MonsterCards { get; set; } = new();
        public List<CharacterDto> CharacterSquad { get; set; } = new();
        public int DieValue { get; set; }
    }
}
