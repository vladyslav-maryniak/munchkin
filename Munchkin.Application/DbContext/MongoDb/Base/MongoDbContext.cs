using MongoDB.Driver;

namespace Munchkin.Application.DbContext.MongoDb.Base
{
    public abstract class MongoDbContext
    {
        private IMongoDatabase? database;

        protected MongoDbContext()
        {
        }

        public MongoDbOptions? Options { get; set; }
        protected IMongoDatabase Database => database ??= InitializeDatabase();

        private IMongoDatabase InitializeDatabase()
        {
            var mongoClient = new MongoClient(Options?.ConnectionString);
            return mongoClient.GetDatabase(Options?.DatabaseName);
        }

        public virtual void OnMapConfiguration(MapRegistrar builder)
        {
        }
    }
}
