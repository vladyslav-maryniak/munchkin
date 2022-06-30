using AutoFixture;
using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;
using Munchkin.Shared.Offers;

namespace Munchkin.Shared.Tests.Builders
{
    public class CombatFieldBuilder
    {
        private readonly CombatField combatField = new();
        private readonly Fixture fixture = new();

        public CombatFieldBuilder WithCharacters(params Character[] characters)
        {
            combatField.CharacterSquad = new(characters);
            return this;
        }

        public CombatFieldBuilder WithCharacters(int count)
        {
            combatField.CharacterSquad = new(fixture.CreateMany<Character>(count));
            return this;
        }

        public CombatFieldBuilder WithMonsters(params MonsterCard[] monsters)
        {
            combatField.MonsterSquad = new(monsters);
            return this;
        }

        public CombatFieldBuilder WithMonsters(int count)
        {
            combatField.MonsterSquad = new(fixture.CreateMany<MonsterCard>());
            return this;
        }

        public CombatFieldBuilder WithReward(Reward reward)
        {
            combatField.Reward = reward;
            return this;
        }

        public CombatFieldBuilder WithCurse(CurseCard curse)
        {
            combatField.CursePlace = curse;
            return this;
        }

        public CombatField Build() => combatField;
    }
}
