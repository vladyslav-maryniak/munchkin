using MediatR;
using Munchkin.Domain.Queries;
using Munchkin.Shared.Cards.Base.Treasures;
using Munchkin.Shared.Events;
using Munchkin.Shared.Events.Base;

namespace Munchkin.Domain.Commands
{
    public static class PlayCard
    {
        public record Command(Guid GameId, Guid PlayerId, Guid CardId) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IMediator mediator;

            public Handler(IMediator mediator)
            {
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = await mediator.Send(new GetGame.Query(request.GameId));
                var game = response.Game;
                var place = game.Table.Places.First(x => x.Player.Id == request.PlayerId);
                var card = place.InHandCards.First(x => x.Id == request.CardId);

                IGameEvent @event = card switch
                {
                    ItemCard => new ItemCardPlayedEvent(
                        request.GameId, request.PlayerId, request.CardId),
                    _ => throw new NotImplementedException()
                };

                await mediator.Send(new PublishEvent.Command(@event));

                return Unit.Value;
            }
        }
    }
}
