using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Munchkin.Application.DbContext.MongoDb.Base;

namespace Munchkin.Application.DbContext.MongoDb.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDbContext<TContext>(
            this IServiceCollection services, Action<MongoDbOptions> optionsAction)
            where TContext : MongoDbContext
        {
            return services.AddMongoDbContext<TContext, TContext>(
optionsAction);
        }

        public static IServiceCollection AddMongoDbContext<TContext, TContextImplementation>(
            this IServiceCollection services, Action<MongoDbOptions> optionsAction)
            where TContext : class
            where TContextImplementation : MongoDbContext, TContext
        {
            MongoDbOptions options = new();
            optionsAction.Invoke(options);

            services.AddSingleton(_ =>
            {
                var mongoClient = new MongoClient(options.ConnectionString);
                return mongoClient.GetDatabase(options.DatabaseName);
            });

            services.AddSingleton<TContext, TContextImplementation>();

            return services;
        }
    }
}
