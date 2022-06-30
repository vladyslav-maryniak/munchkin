using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Domain.Validation;
using Munchkin.Shared.Models;

namespace Munchkin.Domain.Queries
{
    public static class GetPlayer
    {
        public record Query(Guid PlayerId) : IRequest<Response>;

        public class Validator : IValidationHandler<Query>
        {
            private readonly IGameRepository repository;

            public Validator(IGameRepository repository)
            {
                this.repository = repository;
            }

            public async Task<ValidationResult> Validate(Query request)
            {
                var player = await repository.GetPlayerAsync(request.PlayerId);
                return player is null ? ValidationError.InvalidPlayerId : ValidationResult.Success;
            }
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IGameRepository repository;

            public Handler(IGameRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var player = await repository.GetPlayerAsync(request.PlayerId, cancellationToken);

                return new Response() { Player = player };
            }
        }

        public record Response : CqrsResponse
        {
            public Player? Player { get; set; }
        }
    }
}
