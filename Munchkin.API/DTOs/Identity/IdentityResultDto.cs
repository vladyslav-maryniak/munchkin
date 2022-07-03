namespace Munchkin.API.DTOs.Identity
{
    public class IdentityResultDto
    {
        public bool Succeeded { get; set; }
        public IdentityErrorDto[]? Errors { get; set; }
    }
}
