namespace Munchkin.Application.DbContext.MongoDb.Base
{
    public class MongoDbOptions
    {
        public virtual string? ConnectionString { get; set; }
        public virtual string? DatabaseName { get; set; }
    }
}
