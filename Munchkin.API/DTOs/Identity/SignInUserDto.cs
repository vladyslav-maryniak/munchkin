namespace Munchkin.API.DTOs.Identity
{
    public class SignInUserDto
    {
        public string Nickname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsPersistent { get; set; }
    }
}
