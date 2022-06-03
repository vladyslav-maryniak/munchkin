using MongoDB.Bson.Serialization;
using Munchkin.Application.DbContext.MongoDb.Base;
using Munchkin.Shared.Cards.Base;

namespace Munchkin.Application.DbContext.MongoDb.Entities
{
    public class MunchkinCardMapConfiguration : IBsonClassMapConfiguration<MunchkinCard>
    {
        public void Configure(BsonClassMap<MunchkinCard> cm)
        {
            cm.AutoMap();
            cm.SetIsRootClass(true);
            cm.MapIdMember(c => c.Id);
        }
    }
}
