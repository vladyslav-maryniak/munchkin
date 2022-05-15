namespace Munchkin.API.DTOs.Identity
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; } = string.Empty;
    }
}
