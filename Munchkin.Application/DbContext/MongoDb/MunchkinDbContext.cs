using MongoDB.Driver;
using Munchkin.Application.DbContext.MongoDb.Base;
using Munchkin.Application.DbContext.MongoDb.Extensions;
using Munchkin.Shared.Models;
using System.Reflection;

namespace Munchkin.Application.DbContext.MongoDb
{
    public class MunchkinDbContext : MongoDbContext, IMunchkinDbContext
    {
        public IMongoCollection<Game> Games => Database.GetCollection<Game>("Games");
        public IMongoCollection<Player> Players => Database.GetCollection<Player>("Players");

        public override void OnMapConfiguration(MapRegistrar registrar)
        {
            registrar.ApplyMapConfigurationFromAssembly(Assembly.GetExecutingAssembly());
            registrar.ApplyCardMapConfiguration();
        }
    }
}
