using AutoFixture;
using Moq;
using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Cards.Base.Treasures.Items;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Tests.Builders
{
    public class GameBuilder
    {
        private readonly Game game;
        private readonly Fixture fixture = new();

        public GameBuilder()
        {
            var table = new TableBuilder().Build();
            game = new(table);

            fixture.Register(() => Mock.Of<MunchkinCard>());
            fixture.Register(() => Mock.Of<MonsterCard>());
            fixture.Register(() => Mock.Of<CurseCard>());
            fixture.Register(() => Mock.Of<HeadgearCard>());
            fixture.Register(() => Mock.Of<ArmorCard>());
            fixture.Register(() => Mock.Of<FootgearCard>());
            fixture.Register(() => Mock.Of<HandCard>());
        }

        public GameBuilder WithTurnIndex(int turnIndex)
        {
            game.TurnIndex = turnIndex;
            return this;
        }

        public GameBuilder WithTable(Table table)
        {
            game.Table = table;
            return this;
        }

        public GameBuilder WithTable(Func<Fixture, Table> factory)
        {
            game.Table = factory(fixture);
            return this;
        }

        public GameBuilder WithLobby()
        {
            game.Lobby = new LobbyBuilder().Build();
            return this;
        }

        public GameBuilder WithLobby(GameLobby lobby)
        {
            game.Lobby = lobby;
            return this;
        }

        public GameBuilder WithStatus(GameStatus status)
        {
            game.Status = status;
            return this;
        }

        public Game Build() => game;
    }
}
