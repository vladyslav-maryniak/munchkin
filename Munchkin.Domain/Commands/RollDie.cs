using MediatR;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class RollDie
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
                Random r = new();
                var dieValue = r.Next(1, 7);
                var @event = new PlayerRolledDieEvent(request.GameId, request.PlayerId, dieValue);

                await mediator.Send(new PublishEvent.Command(@event));

                return Unit.Value;
            }
        }
    }
}
