using Munchkin.Shared.Models;

namespace Munchkin.Application.Services.Base
{
    public interface IGameRepository
    {
        Task<Game> GetGameAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Player> GetPlayerAsync(Guid id, CancellationToken cancellationToken = default);
    }
}