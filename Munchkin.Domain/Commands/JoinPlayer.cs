using MediatR;
using Munchkin.Domain.Queries;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class JoinPlayer
    {
        public record Command(Guid GameId, Guid PlayerId) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IMediator mediator;

            public Handler(IMediator mediator)
            {
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var gameResponse = await mediator.Send(new GetGame.Query(request.GameId), cancellationToken);
                var playerResponse = await mediator.Send(new GetPlayer.Query(request.PlayerId), cancellationToken);

                if (gameResponse.Game.Lobby.Players.Contains(playerResponse.Player))
                {
                    return Unit.Value;
                }

                var @event = new PlayerJoinedEvent(request.GameId, playerResponse.Player);

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return Unit.Value;
            }
        }
    }
}
