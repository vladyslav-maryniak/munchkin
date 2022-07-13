namespace Munchkin.Shared.Cards.Base.Doors
{
    public abstract class MonsterEnhancerCard : DoorCard
    {
        public abstract int Bonus { get; }
        public abstract int Treasures { get; }
        public virtual IDictionary<string, string> Metadata
            => new Dictionary<string, string>() { ["monsterCardId"] = string.Empty };
    }
}
