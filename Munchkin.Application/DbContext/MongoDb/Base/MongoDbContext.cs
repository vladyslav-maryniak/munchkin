using MongoDB.Driver;

namespace Munchkin.Application.DbContext.MongoDb.Base
{
    public abstract class MongoDbContext
    {
        protected IMongoDatabase Database { get; init; }

        protected MongoDbContext(IMongoDatabase database)
        {
            Database = database;
            OnModelCreating(new());
        }

        public virtual void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
