using MediatR;
using Microsoft.AspNetCore.SignalR;
using Munchkin.Domain.Queries;
using Munchkin.Shared.Events;
using Munchkin.Shared.Hubs;

namespace Munchkin.Domain.Commands
{
    public static class JoinPlayer
    {
        public record Command(Guid GameId, Guid PlayerId) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IMediator mediator;
            private readonly IHubContext<EventHub> hub;

            public Handler(IMediator mediator, IHubContext<EventHub> hub)
            {
                this.mediator = mediator;
                this.hub = hub;
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
                await hub.Clients.All.SendAsync(request.GameId.ToString(), nameof(PlayerJoinedEvent), cancellationToken);

                return Unit.Value;
            }
        }
    }
}
