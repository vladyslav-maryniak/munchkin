using AutoFixture;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Tests.Builders
{
    public class LobbyBuilder
    {
        private readonly GameLobby lobby = new();
        private Fixture fixture = new();

        public LobbyBuilder WithPlayers(int count)
        {
            lobby.Players = new(fixture.CreateMany<Player>(count));
            return this;
        }

        public GameLobby Build() => lobby;
    }
}
