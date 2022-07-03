using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Domain.Queries;
using Munchkin.Domain.Validation;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class JoinPlayer
    {
        public record Command(Guid GameId, Guid PlayerId) : IRequest<Response>;

        public class Validator : IValidationHandler<Command>
        {
            private readonly IGameRepository repository;

            public Validator(IGameRepository repository)
            {
                this.repository = repository;
            }

            public async Task<ValidationResult> Validate(Command request)
            {
                var game = await repository.GetGameAsync(request.GameId);

                if (game is null)
                {
                    return ValidationError.NoGame;
                }

                if (game.Lobby.Players.Any(x => x.Id == request.PlayerId))
                {
                    return ValidationError.PlayerInLobby;
                }

                return ValidationResult.Success;
            }
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IMediator mediator;

            public Handler(IMediator mediator)
            {
                this.mediator = mediator;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var gameResponse = await mediator.Send(new GetGame.Query(request.GameId), cancellationToken);
                var playerResponse = await mediator.Send(new GetPlayer.Query(request.PlayerId), cancellationToken);
                var game = gameResponse.Game!;
                var player = playerResponse.Player!;

                if (game.Lobby.Players.Contains(player))
                {
                    return new Response();
                }

                var @event = new PlayerJoinedEvent(request.GameId, player);

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return new Response();
            }
        }

        public record Response : CqrsResponse;
    }
}
