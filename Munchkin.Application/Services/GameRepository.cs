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
        private readonly List<Game> games = new();

        private readonly List<Player> players = new()
        {
            new(Guid.Parse("a0cfe7fc-b71c-4129-ab11-6346efdbe0ed"), "Bob"),
            new(Guid.Parse("0c8380d2-17e9-404b-9f24-5bc36536db86"), "Martin"),
            new(Guid.Parse("2e79c0b3-b9c3-442f-b480-731291141337"), "Julie"),
        };

        public Task<Game> CreateGameAsync(CancellationToken cancellationToken = default)
        {
            var doorDeck = GetDoorDeck();
            var treasureDeck = GetTreasureDeck();
            var table = new Table(doorDeck, treasureDeck);

            var game = new Game(table);

            games.Add(game);
            return Task.FromResult(game);
        }

        public Task<Game> GetGameAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(games.First(x => x.Id == id));
        }

        public Task<Player> GetPlayerAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(players.First(x => x.Id == id));
        }

        private static Stack<DoorCard> GetDoorDeck()
        {
            Stack<DoorCard> doorDeck = new();

            for (int i = 0; i < 4; i++)
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

            for (int i = 0; i < 3; i++)
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
