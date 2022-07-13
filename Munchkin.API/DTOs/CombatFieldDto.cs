namespace Munchkin.API.DTOs
{
    public class CombatFieldDto
    {
        public List<object> MonsterSquad { get; set; } = new();
        public Dictionary<Guid, object[]> MonsterEnhancers { get; set; } = new();
        public List<CharacterDto> CharacterSquad { get; set; } = new();
        public object? Reward { get; set; } = new();
        public object? CursePlace { get; set; }
    }
}
