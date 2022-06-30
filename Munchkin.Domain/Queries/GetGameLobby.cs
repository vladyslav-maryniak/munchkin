using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Domain.Validation;
using Munchkin.Shared.Models;

namespace Munchkin.Domain.Queries
{
    public static class GetGameLobby
    {
        public record Query(Guid GameId) : IRequest<Response>;

        public class Validator : IValidationHandler<Query>
        {
            private readonly IGameRepository repository;

            public Validator(IGameRepository repository)
            {
                this.repository = repository;
            }

            public async Task<ValidationResult> Validate(Query request)
            {
                var game = await repository.GetGameAsync(request.GameId);
                return game is null ? ValidationError.NoGame : ValidationResult.Success;
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
                var lobby = await repository.GetGameLobbyAsync(request.GameId, cancellationToken);

                return new Response() { Lobby = lobby };
            }
        }

        public record Response : CqrsResponse
        {
            public GameLobby? Lobby { get; set; }
        }
    }
}
