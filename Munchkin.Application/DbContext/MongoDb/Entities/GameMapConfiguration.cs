using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Munchkin.Application.DbContext.MongoDb.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Application.DbContext.MongoDb.Entities
{
    internal class GameMapConfiguration : IBsonClassMapConfiguration<Game>
    {
        public void Configure(BsonClassMap<Game> cm)
        {
            cm.AutoMap();
            cm.MapCreator(c => new Game(c.Table));
            cm.MapIdMember(c => c.Id);
            cm.MapMember(c => c.Status).SetSerializer(new EnumSerializer<GameStatus>(BsonType.String));
        }
    }
}
