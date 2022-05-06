﻿using MediatR;
using Munchkin.Domain.Queries;
using Munchkin.Shared.Events;
using Munchkin.Shared.Events.Base;

namespace Munchkin.Domain.Commands
{
    public static class InitiateCombat
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
                var character = game.Table.Places
                    .First(x => x.Character.Id == request.CharacterId)
                    .Character;

                var monsterCombatStrength = game.Table.CombatField.MonsterSquad
                    .Select(x => x.Level)
                    .Aggregate((result, x) => result + x);
                var characterCombatStrength = character.Level + character.Equipment.Bonus;

                IGameEvent @event = characterCombatStrength > monsterCombatStrength ?
                    new CharacterWonCombatEvent(request.GameId, request.CharacterId) :
                    new CharacterAskedForHelpEvent(request.GameId, request.CharacterId);

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return Unit.Value;
            }
        }
    }
}
