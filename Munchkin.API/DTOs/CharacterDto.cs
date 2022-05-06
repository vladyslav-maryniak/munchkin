namespace Munchkin.API.DTOs
{
    public class CharacterDto
    {
        public Guid Id { get; set; }
        public int Level { get; set; }
        public EquipmentDto Equipment { get; set; } = new();
    }
}
