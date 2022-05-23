using MongoDB.Bson.Serialization.Attributes;

namespace Munchkin.Shared.Cards.Base
{
    public abstract class MunchkinCard
    {
        [BsonId]
        public Guid Id { get; set; }
        public abstract string Name { get; }
        public abstract string Description { get; }

        protected MunchkinCard()
        {
            Id = Guid.NewGuid();
        }
    }
}
