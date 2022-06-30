using MongoDB.Bson.Serialization;
using Munchkin.Application.DbContext.MongoDb.Base;
using Munchkin.Shared.Offers;

namespace Munchkin.Application.DbContext.MongoDb.Entities
{
    public class OfferMapConfiguration : IBsonClassMapConfiguration<Offer>
    {
        public void Configure(BsonClassMap<Offer> cm)
        {
            cm.AutoMap();
            cm.SetIsRootClass(true);
            cm.MapIdMember(x => x.Id);
        }
    }
}
