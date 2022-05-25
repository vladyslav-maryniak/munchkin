using MongoDB.Bson.Serialization;
using Munchkin.Application.DbContext.MongoDb.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Application.DbContext.MongoDb.Entities
{
    public class CharacterMapConfiguration : IBsonClassMapConfiguration<Character>
    {
        public void Configure(BsonClassMap<Character> cm)
        {
            cm.AutoMap();
            cm.MapIdMember(c => c.Id);
        }
    }
}
