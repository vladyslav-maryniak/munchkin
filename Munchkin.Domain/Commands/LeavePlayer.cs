using MediatR;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public class LeavePlayer
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
                var @event = new PlayerLeftEvent(request.GameId, request.PlayerId);

                await mediator.Send(new PublishEvent.Command(@event));

                return Unit.Value;
            }
        }
    }
}
