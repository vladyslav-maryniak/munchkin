using Moq;
using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Events;
using Munchkin.Shared.Models;
using Munchkin.Shared.Offers;
using Munchkin.Shared.Tests.Attributes;
using Munchkin.Shared.Tests.Builders;
using Shouldly;

namespace Munchkin.Shared.Tests.Events
{
    public class CharacterWonCombatEventTests
    {
        [Theory, AutoGameData]
        public void CharacterWonCombatEvent_ShouldAddVictoryTreasuresToInHandCardsOfWinner(
            Place winner, MonsterCard firstMonster, MonsterCard secondMonster)
        {
            // Arrange
            var combatField = new CombatFieldBuilder()
                .WithMonsters(firstMonster, secondMonster)
                .Build();
            int numberOfWinningTreasureCards = firstMonster.Treasures + secondMonster.Treasures;
            var table = new TableBuilder()
                .WithPlaces(winner)
                .WithCombatField(combatField)
                .WithTreasureDeck(numberOfWinningTreasureCards)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();
            int initialNumberOfInHandCards = winner.InHandCards.Count;

            // Act
            CharacterWonCombatEvent @event = new(game.Id, winner.Character.Id);
            @event.Apply(game);

            // Assert
            int difference = winner.InHandCards.Count - initialNumberOfInHandCards;
            difference.ShouldBe(numberOfWinningTreasureCards);
        }

        [Theory, AutoGameData]
        public void CharacterWonCombatEvent_ShouldPerformReward_WhenRewardIsIntendedAndOffereeIsAssigned(
            Place hero, Place helper)
        {
            // Arrange
            var reward = new RewardBuilder(Mock.Of<Reward>())
                .WithOfferorId(hero.Player.Id)
                .WithOffereeId(helper.Player.Id)
                .Build();
            var combatField = new CombatFieldBuilder()
                .WithReward(reward)
                .WithMonsters(Mock.Of<MonsterCard>())
                .Build();
            var table = new TableBuilder()
                .WithPlaces(hero, helper)
                .WithCombatField(combatField)
                .Build();
            var game = new GameBuilder()
                .WithTable(table)
                .Build();

            // Act
            CharacterWonCombatEvent @event = new(game.Id, hero.Character.Id);
            @event.Apply(game);

            // Arrange
            Mock.Get(reward).Verify(m => m.Perform(game), Times.Once);
        }
    }
}
