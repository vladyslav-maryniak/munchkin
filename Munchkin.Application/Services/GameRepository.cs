using MongoDB.Driver;
using Munchkin.Application.DbContext.MongoDb.Base;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Cards;
using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Extensions;
using Munchkin.Shared.Models;

namespace Munchkin.Application.Services
{
    public class GameRepository : IGameRepository
    {
        private readonly IMunchkinDbContext context;

        public GameRepository(IMunchkinDbContext context)
        {
            this.context = context;
        }

        public async Task<Game> CreateGameAsync(CancellationToken cancellationToken = default)
        {
            var doorDeck = GetDoorDeck();
            var treasureDeck = GetTreasureDeck();
            var table = new Table(doorDeck, treasureDeck);
            var game = new Game(table);

            await context.Games.InsertOneAsync(game, new(), cancellationToken);

            return game;
        }

        public async Task<Player> CreatePlayerAsync(Guid playerId, string nickname, CancellationToken cancellationToken = default)
        {
            var player = new Player(playerId, nickname);

            await context.Players.InsertOneAsync(player, new(), cancellationToken);

            return player;
        }

        public Task<Game> GetGameAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return context.Games.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<GameLobby> GetGameLobbyAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var game = await context.Games.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
            return game.Lobby;
        }

        public Task<Player> GetPlayerAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return context.Players.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Game>> GetPlayerGamesAsync(Guid playerId, CancellationToken cancellationToken = default)
        {
            return await context.Games
                .Find(x => x.Table.Places.Any(x => x.Player.Id == playerId) || x.Lobby.Players.Any(x => x.Id == playerId))
                .ToListAsync(cancellationToken);
        }

        public Task UpdateGameAsync(Game game, CancellationToken cancellationToken = default)
        {
            return context.Games.ReplaceOneAsync(x => x.Id == game.Id, game, cancellationToken: cancellationToken);
        }

        private static Stack<DoorCard> GetDoorDeck()
            => new(MunchkinCards.DoorCards.Shuffle());

        private static Stack<TreasureCard> GetTreasureDeck()
            => new(MunchkinCards.TreasureCards.Shuffle());
    }
}
