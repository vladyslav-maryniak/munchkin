using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Domain.Queries
{
    public static class GetPlayer
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
                var player = await repository.GetPlayerAsync(request.PlayerId);
                return new Response(player);
            }
        }

        public record Response(Player Player);
    }
}
