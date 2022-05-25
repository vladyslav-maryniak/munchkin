using MongoDB.Driver;
using Munchkin.Shared.Models;

namespace Munchkin.Application.DbContext.MongoDb.Base
{
    public interface IMunchkinDbContext
    {
        IMongoCollection<Game> Games { get; }
        IMongoCollection<Player> Players { get; }
    }
}