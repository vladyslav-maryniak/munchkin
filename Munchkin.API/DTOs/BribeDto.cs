namespace Munchkin.API.DTOs
{
    public class BribeDto : OfferDto
    {
        public string Agreement { get; set; } = string.Empty;
        public Guid[] ItemCardIds { get; set; } = Array.Empty<Guid>();
    }
}
