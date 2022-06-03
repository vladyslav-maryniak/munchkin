namespace Munchkin.API.DTOs
{
    public class CardSaleEventDto
    {
        public Guid PlayerId { get; set; }
        public IEnumerable<Guid> CardIds { get; set; } = new List<Guid>();
    }
}
