using MediatR;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class OfferBribe
    {
        public record Command(
            Guid GameId,
            Guid OfferorId,
            Guid OffereeId,
            string Agreement,
            Guid[] EncourageItemCardIds) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IMediator mediator;

            public Handler(IMediator mediator)
            {
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var @event = new PlayerOfferedBribeEvent(
                    request.GameId,
                    request.OfferorId,
                    request.OffereeId,
                    request.Agreement,
                    request.EncourageItemCardIds);

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return Unit.Value;
            }
        }
    }
}
