using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Domain.Queries
{
    public static class GetGameLobby
    {
        public record Query(Guid GameId) : IRequest<Response>;

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IGameRepository repository;

            public Handler(IGameRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var lobby = await repository.GetGameLobbyAsync(request.GameId, cancellationToken);

                return new Response(lobby);
            }
        }

        public record Response(GameLobby Lobby);
    }
}
