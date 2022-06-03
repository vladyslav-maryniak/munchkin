using MongoDB.Bson.Serialization;
using Munchkin.Application.DbContext.MongoDb.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Application.DbContext.MongoDb.Entities
{
    public class TableMapConfiguration : IBsonClassMapConfiguration<Table>
    {
        public void Configure(BsonClassMap<Table> cm)
        {
            cm.AutoMap();
            cm.MapCreator(c => new Table(c.DoorDeck, c.TreasureDeck));
        }
    }
}
