namespace Munchkin.Shared.Cards.Base
{
    public abstract class MunchkinCard
    {
        public abstract Guid Id { get; set; }
        public abstract string Name { get; }
        public abstract string Description { get; }
    }
}
