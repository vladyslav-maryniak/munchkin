namespace Munchkin.Application.DbContext.MongoDb.Base
{
    public class MongoDbContextBuilder<TContextImplementation>
        where TContextImplementation : MongoDbContext, new()
    {
        private readonly TContextImplementation context;

        public MongoDbContextBuilder()
        {
            context = new();
        }

        public MongoDbContextBuilder<TContextImplementation> WithOptions(MongoDbOptions options)
        {
            context.Options = options;
            return this;
        }

        public MongoDbContextBuilder<TContextImplementation> WithMapConfiguration(MapRegistrar registrar)
        {
            context.OnMapConfiguration(registrar);
            return this;
        }

        public TContextImplementation Build() => context;
    }
}
