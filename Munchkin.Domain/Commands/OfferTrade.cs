using MediatR;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class OfferTrade
    {
        public record Command(
            Guid GameId,
            Guid OfferorId,
            Guid OffereeId,
            Guid[] OfferorItemCardIds,
            Guid[] OffereeItemCardIds) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IMediator mediator;

            public Handler(IMediator mediator)
            {
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var @event = new PlayerOfferedTradeEvent(
                    request.GameId,
                    request.OfferorId,
                    request.OffereeId,
                    request.OfferorItemCardIds,
                    request.OffereeItemCardIds);

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return Unit.Value;
            }
        }
    }
}
