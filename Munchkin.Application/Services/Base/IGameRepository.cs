using Munchkin.Shared.Models;

namespace Munchkin.Application.Services.Base
{
    public interface IGameRepository
    {
        Task<Game> CreateGameAsync(CancellationToken cancellationToken = default);
        Task<Player> CreatePlayerAsync(
            Guid playerId, string nickname, CancellationToken cancellationToken = default);
        Task<Game> GetGameAsync(Guid id, CancellationToken cancellationToken = default);
        Task<GameLobby> GetGameLobbyAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Player> GetPlayerAsync(Guid id, CancellationToken cancellationToken = default);
    }
}