namespace Munchkin.Infrastucture.Projections
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }

        public Player(Guid id, string nickname)
        {
            Id = id;
            Nickname = nickname;
        }
    }
}
