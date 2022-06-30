using MediatR;
using Munchkin.Domain.Queries;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class OfferReward
    {
        public record Command(
        Guid GameId,
        Guid OfferorId,
        Guid[] ItemCardIds,
        Guid[] CardIdsForPlay,
        int NumberOfTreasures,
        bool HelperPicksFirst) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IMediator mediator;

            public Handler(IMediator mediator)
            {
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = await mediator.Send(new GetGame.Query(request.GameId), cancellationToken);
                var game = response.Game;
                var victoryTreasures = game.Table.CombatField.MonsterSquad
                    .Select(x => x.Treasures)
                    .Aggregate((result, x) => result + x);

                var @event = new PlayerOfferedRewardEvent(
                    request.GameId,
                    request.OfferorId,
                    request.ItemCardIds,
                    request.CardIdsForPlay,
                    victoryTreasures,
                    request.NumberOfTreasures,
                    request.HelperPicksFirst);

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return Unit.Value;
            }
        }
    }
}
