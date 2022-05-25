using MediatR;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class UpdateGameState
    {
        public record Command(Guid GameId, string State) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IMediator mediator;

            public Handler(IMediator mediator)
            {
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var @event = new GameStateUpdated(request.GameId, request.State);

                await mediator.Send(new PublishEvent.Command(@event));

                return Unit.Value;
            }
        }
    }
}
