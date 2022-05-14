namespace Munchkin.API.Options
{
    public class MongoDbOptions
    {
        public string Name { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}
