namespace Munchkin.API.DTOs
{
    public class OfferDto
    {
        public Guid Id { get; set; }
        public Guid OfferorId { get; set; } = new();
        public Guid OffereeId { get; set; } = new();
    }
}
