namespace Munchkin.API.DTOs
{
    public class MonsterCardDto
    {
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
        public string BadStuff { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int VictoryLevels { get; set; }
    }
}
