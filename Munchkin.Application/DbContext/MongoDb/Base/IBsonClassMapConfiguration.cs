using MongoDB.Bson.Serialization;

namespace Munchkin.Application.DbContext.MongoDb.Base
{
    public interface IBsonClassMapConfiguration<T>
        where T : class
    {
        void Configure(BsonClassMap<T> cm);
    }
}