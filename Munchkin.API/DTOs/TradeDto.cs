namespace Munchkin.API.DTOs
{
    public class TradeDto : OfferDto
    {
        public Guid[] OfferorItemCardIds { get; set; } = Array.Empty<Guid>();
        public Guid[] OffereeItemCardIds { get; set; } = Array.Empty<Guid>();
    }
}
