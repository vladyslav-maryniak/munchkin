namespace Munchkin.API.DTOs
{
    public class TableDto
    {
        public List<PlaceDto> Places { get; set; } = new();
        public CombatFieldDto CombatField { get; set; } = new();
        public int DieValue { get; set; }
    }
}
