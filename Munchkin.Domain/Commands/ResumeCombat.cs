using MediatR;
using Munchkin.Domain.Queries;
using Munchkin.Shared.Events;
using Munchkin.Shared.Events.Base;

namespace Munchkin.Domain.Commands
{
    public static class ResumeCombat
    {
        public record Command(Guid GameId, Guid CharacterId) : IRequest;

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

                IGameEvent @event = squadCombatStrength > monsterCombatStrength ?
                    new CharacterWonCombatEvent(request.GameId, request.CharacterId) :
                    new CharacterRanAwayEvent(request.GameId, request.CharacterId);

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return Unit.Value;
            }
        }
    }
}
