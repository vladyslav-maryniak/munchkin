using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Domain.Queries
{
    public static class GetGame
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
                var game = await repository.GetGameAsync(request.GameId);
                return new Response(game);
            }
        }

        public record Response(Game Game);
    }
}
