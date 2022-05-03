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
                var response = await mediator.Send(new GetGame.Query(request.GameId));
                var game = response.Game;

                var monsterCombatStrength = game.Table.MonsterCards
                    .Select(x => x.Level)
                    .Aggregate((result, x) => result + x);
                var squadCombatStrength = game.Table.CharacterSquad
                    .Select(x => x.Level)
                    .Aggregate((result, x) => result + x);

                IGameEvent @event = squadCombatStrength > monsterCombatStrength ?
                    new CharacterWonCombatEvent(request.GameId, request.CharacterId) :
                    new CharacterRanAwayEvent(request.GameId, request.CharacterId);

                await mediator.Send(new PublishEvent.Command(@event));

                return Unit.Value;
            }
        }
    }
}
