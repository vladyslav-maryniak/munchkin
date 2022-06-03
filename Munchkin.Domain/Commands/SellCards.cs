using MediatR;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class SellCards
    {
        public record Command(Guid GameId, Guid PlayerId, IEnumerable<Guid> CardIds) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IMediator mediator;

            public Handler(IMediator mediator)
            {
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var @event = new PlayerSoldCardsEvent(request.GameId, request.PlayerId, request.CardIds);

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return Unit.Value;
            }
        }
    }
}
