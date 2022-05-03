using Munchkin.Shared.Projections;

namespace Munchkin.Application.Services.Base
{
    public interface IGameRepository
    {
        Task<Game> GetGameAsync(Guid id);
        Task<Player> GetPlayerAsync(Guid id);
    }
}