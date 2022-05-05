namespace Munchkin.API.DTOs
{
    public class CombatFieldDto
    {
        public List<MonsterCardDto> MonsterSquad { get; set; } = new();
        public List<CharacterDto> CharacterSquad { get; set; } = new();
    }
}
