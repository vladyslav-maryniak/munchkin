using MongoDB.Bson.Serialization;
using Munchkin.Application.DbContext.MongoDb.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Application.DbContext.MongoDb.Entities
{
    public class PlayerMapConfiguration : IBsonClassMapConfiguration<Player>
    {
        public void Configure(BsonClassMap<Player> cm)
        {
            cm.AutoMap();
            cm.MapCreator(c => new Player(c.Id, c.Nickname));
            cm.MapIdMember(c => c.Id);
        }
    }
}
