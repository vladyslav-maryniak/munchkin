namespace Munchkin.Shared.Models
{
    public class Player : IEquatable<Player>
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }

        public Player(Guid id, string nickname)
        {
            Id = id;
            Nickname = nickname;
        }

        public bool Equals(Player? other)
        {
            return Id == other?.Id;
        }
    }
}
