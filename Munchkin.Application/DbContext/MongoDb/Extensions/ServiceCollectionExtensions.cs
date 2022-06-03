using Microsoft.Extensions.DependencyInjection;
using Munchkin.Application.DbContext.MongoDb.Base;

namespace Munchkin.Application.DbContext.MongoDb.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDbContext<TContext>(
            this IServiceCollection services, Action<MongoDbOptions> optionsAction)
            where TContext : MongoDbContext, new()
        {
            return services.AddMongoDbContext<TContext, TContext>(optionsAction);
        }

        public static IServiceCollection AddMongoDbContext<TContext, TContextImplementation>(
            this IServiceCollection services, Action<MongoDbOptions> optionsAction)
            where TContext : class
            where TContextImplementation : MongoDbContext, TContext, new()
        {
            MongoDbOptions options = new();
            optionsAction.Invoke(options);

            services.AddSingleton(options);
            services.AddSingleton<TContext, TContextImplementation>(
                provider => new MongoDbContextBuilder<TContextImplementation>()
                    .WithOptions(options)
                    .WithMapConfiguration(new MapRegistrar())
                    .Build());

            return services;
        }
    }
}
