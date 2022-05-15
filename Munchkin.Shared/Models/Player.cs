using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace Munchkin.Shared.Models
{
    [CollectionName("Players")]
    public class Player : IEquatable<Player>
    {
        [BsonId]
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
