namespace Munchkin.API.DTOs
{
    public class RewardDto : OfferDto
    {
        public Guid[] ItemCardIds { get; set; } = Array.Empty<Guid>();
        public Guid[] CardIdsForPlay { get; set; } = Array.Empty<Guid>();
        public int NumberOfTreasures { get; set; }
        public bool HelperPicksFirst { get; set; }
    }
}
