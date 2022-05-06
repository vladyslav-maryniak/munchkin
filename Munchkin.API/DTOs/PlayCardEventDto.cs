namespace Munchkin.API.DTOs
{
    public class PlayCardEventDto
    {
        public Guid PlayerId { get; set; }
        public Guid CardId { get; set; }
        public Dictionary<string, string> MetaData { get; set; } = new();
    }
}
