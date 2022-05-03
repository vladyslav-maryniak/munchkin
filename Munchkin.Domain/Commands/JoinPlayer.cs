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
                var response = await mediator.Send(new GetPlayer.Query(request.PlayerId));
                var @event = new PlayerJoinedEvent(request.GameId, response.Player);

                await mediator.Send(new PublishEvent.Command(@event));

                return Unit.Value;
            }
        }
    }
}
