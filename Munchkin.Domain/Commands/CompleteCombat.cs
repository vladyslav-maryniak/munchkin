using MediatR;
using Munchkin.Domain.Queries;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class CompleteCombat
    {
        public record Command(Guid GameId) : IRequest;

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

                var monsterCombatStrength = game.Table.CombatField.MonsterSquad
                    .Select(x => x.Level)
                    .Aggregate((result, x) => result + x);

                var characterSquad = game.Table.CombatField.CharacterSquad;
                var squadCombatStrength =
                    characterSquad
                        .Select(x => x.Level)
                        .Aggregate((result, x) => result + x) +
                    characterSquad
                        .Select(x => x.Equipment.Bonus)
                        .Aggregate((result, x) => result + x);

                var reward = game.Table.CombatField.Reward;
                if (reward is not null && squadCombatStrength > monsterCombatStrength)
                {
                    foreach (var cardId in reward.CardIdsForPlay)
                    {
                        await mediator.Send(new PlayCard.Command(request.GameId, reward.OffereeId, cardId));
                    }
                }

                var @event = new CombatCompletedEvent(request.GameId);

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return Unit.Value;
            }
        }
    }
}
