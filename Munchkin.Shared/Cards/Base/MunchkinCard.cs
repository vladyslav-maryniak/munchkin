namespace Munchkin.Shared.Cards.Base
{
    public abstract class MunchkinCard
    {
        public Guid Id { get; set; }
        public abstract string Name { get; }
        public abstract string Description { get; }

        protected MunchkinCard()
        {
            Id = Guid.NewGuid();
        }
    }
}
