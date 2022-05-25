using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace Munchkin.Shared.Models
{
    [CollectionName("Games")]
    public class Game
    {
        [BsonId]
        public Guid Id { get; set; }
        public int TurnIndex { get; set; }
        public string? State { get; set; }
        public GameLobby Lobby { get; set; } = new();
        public Table Table { get; set; }

        public Game(Table table)
        {
            Table = table;
        }

        public bool IsPlayerTurn(Guid playerId)
            => Table.Places[TurnIndex].Player.Id == playerId;
    }
}
