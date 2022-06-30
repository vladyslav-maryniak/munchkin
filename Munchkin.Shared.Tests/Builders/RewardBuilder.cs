using Munchkin.Shared.Offers;

namespace Munchkin.Shared.Tests.Builders
{
    public class RewardBuilder
    {
        private readonly Reward reward = new();

        public RewardBuilder()
        {
        }

        public RewardBuilder(Reward reward)
        {
            this.reward = reward;
        }

        public RewardBuilder WithOfferorId(Guid id)
        {
            reward.OfferorId = id;
            return this;
        }

        public RewardBuilder WithOffereeId(Guid id)
        {
            reward.OffereeId = id;
            return this;
        }

        public Reward Build() => reward;
    }
}
