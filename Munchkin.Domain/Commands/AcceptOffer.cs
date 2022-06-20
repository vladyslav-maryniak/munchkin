using MediatR;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class AcceptOffer
    {
        public record Command(Guid GameId, Guid OfferId) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IMediator mediator;

            public Handler(IMediator mediator)
            {
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var @event = new PlayerAcceptedOfferEvent(request.GameId, request.OfferId);

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return Unit.Value;
            }
        }
    }
}
