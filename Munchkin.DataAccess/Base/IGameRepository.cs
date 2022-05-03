using Munchkin.Infrastucture.Projections;

namespace Munchkin.DataAccess.Base
{
    public interface IGameRepository
    {
        Task<Game> GetGameAsync(Guid id);
        Task<Player> GetPlayerAsync(Guid id);
    }
}