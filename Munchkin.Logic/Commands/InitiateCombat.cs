using MediatR;
using Munchkin.DataAccess.Base;
using Munchkin.Infrastucture.Events;
using Munchkin.Infrastucture.Events.Base;

namespace Munchkin.Logic.Commands
{
    public static class InitiateCombat
    {
        public record Command(Guid GameId, Guid PlayerId) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IEventService service;
            private readonly IEventRepository repository;

            public Handler(IEventService service, IEventRepository repository)
            {
                this.service = service;
                this.repository = repository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var game = await repository.GetGameAsync(request.GameId);
                var character = game.Characters.First(x => x.Player.Id == request.PlayerId);
                
                var monsterCombatStrength = game.Table.MonsterCards
                    .Select(x => x.Level)
                    .Aggregate((result, x) => result + x);
                var characterCombatStrength = character.Level;

                IGameEvent @event = characterCombatStrength > monsterCombatStrength ?
                    new PlayerWonCombatEvent(request.GameId, request.PlayerId) :
                    new PlayerAskedForHelpEvent(request.GameId, request.PlayerId);

                await service.PublishAsync(@event);

                return Unit.Value;
            }
        }
    }
}
