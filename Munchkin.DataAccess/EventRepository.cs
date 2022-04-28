using Munchkin.Infrastucture.Projections;

namespace Munchkin.DataAccess
{
    public class EventRepository : IEventRepository
    {
        private readonly List<Game> games = new() { new(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")) };

        public Game GetGame(Guid id) => games.First(x => x.Id == id);
    }
}
