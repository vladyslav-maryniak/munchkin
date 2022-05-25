using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Domain.Queries
{
    public static class GetPlayerGames
    {
        public record Query(Guid PlayerId) : IRequest<Response>;

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IGameRepository repository;

            public Handler(IGameRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var games = await repository.GetPlayerGamesAsync(request.PlayerId, cancellationToken);
                return new Response(games);
            }
        }

        public record Response(List<Game> Games);
    }
}
