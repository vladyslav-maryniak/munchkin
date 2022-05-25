using MongoDB.Bson.Serialization;
using Munchkin.Application.DbContext.MongoDb.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Application.DbContext.MongoDb.Entities
{
    public class PlaceMapConfiguration : IBsonClassMapConfiguration<Place>
    {
        public void Configure(BsonClassMap<Place> cm)
        {
            cm.AutoMap();
            cm.MapCreator(c => new Place(c.Player, c.Character));
        }
    }
}
