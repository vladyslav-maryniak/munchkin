using Munchkin.DataAccess.Base;
using Munchkin.Infrastucture.Cards.Base;
using Munchkin.Infrastucture.Cards.Doors.Curses;
using Munchkin.Infrastucture.Cards.Doors.Monsters;
using Munchkin.Infrastucture.Projections;

namespace Munchkin.DataAccess
{
    public class EventRepository : IEventRepository
    {
        private readonly List<Game> games = new()
        {
            new Game
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                DoorDeck = new(new DoorCard[]
                {
                    new DuckOfDoom(),
                    new UndeadHorse(),
                    new FlyingFrogs(),
                    new MaulRat(),
                })
            }
        };

        private readonly List<Player> players = new()
        {
            new(Guid.Parse("a0cfe7fc-b71c-4129-ab11-6346efdbe0ed"), "Bob"),
            new(Guid.Parse("0c8380d2-17e9-404b-9f24-5bc36536db86"), "Martin"),
            new(Guid.Parse("2e79c0b3-b9c3-442f-b480-731291141337"), "Julie"),
        };

        public Task<Game> GetGameAsync(Guid id)
        {
            return Task.FromResult(games.First(x => x.Id == id));
        }

        public Task<Player> GetPlayerAsync(Guid id)
        {
            return Task.FromResult(players.First(x => x.Id == id));
        }
    }
}
