using MongoDB.Driver;
using Munchkin.Application.DbContext.MongoDb.Base;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Cards.Doors.Curses;
using Munchkin.Shared.Cards.Doors.Monsters;
using Munchkin.Shared.Cards.Treasures.GoUpLevels;
using Munchkin.Shared.Cards.Treasures.Items.Armor;
using Munchkin.Shared.Cards.Treasures.Items.Footgear;
using Munchkin.Shared.Cards.Treasures.Items.Headgear;
using Munchkin.Shared.Cards.Treasures.Items.OneHand;
using Munchkin.Shared.Cards.Treasures.Items.TwoHands;
using Munchkin.Shared.Cards.Treasures.OneShots;
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

        public Task UpdateGameAsync(Game game, CancellationToken cancellationToken = default)
        {
            return context.Games.ReplaceOneAsync(x => x.Id == game.Id, game, cancellationToken: cancellationToken);
        }

        private static Stack<DoorCard> GetDoorDeck()
        {
            Stack<DoorCard> doorDeck = new();

            for (int i = 0; i < 10; i++)
            {
                doorDeck.Push(new DuckOfDoom());
                doorDeck.Push(new UndeadHorse());
                doorDeck.Push(new FlyingFrogs());
                doorDeck.Push(new MaulRat());
            }

            return doorDeck;
        }

        private static Stack<TreasureCard> GetTreasureDeck()
        {
            Stack<TreasureCard> treasureDeck = new();

            for (int i = 0; i < 10; i++)
            {
                treasureDeck.Push(new InvokeObscureRules());
                treasureDeck.Push(new FlamingArmor());
                treasureDeck.Push(new BootsOfButtKicking());
                treasureDeck.Push(new HelmOfCourage());
                treasureDeck.Push(new LoadedDie());
                treasureDeck.Push(new ConvenientAdditionError());
                treasureDeck.Push(new SneakyBastardSword());
                treasureDeck.Push(new ElevenFootPole());
                treasureDeck.Push(new Doppleganger());
                treasureDeck.Push(new BoilAnAnthill());
            }

            return treasureDeck;
        }
    }
}
