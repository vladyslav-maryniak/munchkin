using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Events;
using Munchkin.Shared.Events.Base;

namespace Munchkin.Domain.Commands
{
    public static class InitiateCombat
    {
        public record Command(Guid GameId, Guid CharacterId) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IEventService service;
            private readonly IGameRepository repository;

            public Handler(IEventService service, IGameRepository repository)
            {
                this.service = service;
                this.repository = repository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var game = await repository.GetGameAsync(request.GameId);
                var character = game.Characters.First(x => x.Id == request.CharacterId);

                var monsterCombatStrength = game.Table.MonsterCards
                    .Select(x => x.Level)
                    .Aggregate((result, x) => result + x);
                var characterCombatStrength = character.Level;

                IGameEvent @event = characterCombatStrength > monsterCombatStrength ?
                    new CharacterWonCombatEvent(request.GameId, request.CharacterId) :
                    new CharacterAskedForHelpEvent(request.GameId, request.CharacterId);

                await service.PublishAsync(@event);

                return Unit.Value;
            }
        }
    }
}
