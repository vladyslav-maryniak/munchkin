using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Domain.Queries;
using Munchkin.Domain.Validation;
using Munchkin.Shared.Events;
using Munchkin.Shared.Events.Base;

namespace Munchkin.Domain.Commands
{
    public static class ResumeCombat
    {
        public record Command(Guid GameId, Guid CharacterId) : IRequest<Response>;

        public class Validator : IValidationHandler<Command>
        {
            private readonly IGameRepository repository;

            public Validator(IGameRepository repository)
            {
                this.repository = repository;
            }

            public async Task<ValidationResult> Validate(Command request)
            {
                var game = await repository.GetGameAsync(request.GameId);

                if (game is null)
                {
                    return ValidationError.NoGame;
                }

                if (!game.Table.Places.Any(x => x.Character.Id == request.CharacterId))
                {
                    return ValidationError.NoCharacter;
                }

                return ValidationResult.Success;
            }
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IMediator mediator;

            public Handler(IMediator mediator)
            {
                this.mediator = mediator;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = await mediator.Send(new GetGame.Query(request.GameId), cancellationToken);
                var game = response.Game!;

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

                return new Response();
            }
        }

        public record Response : CqrsResponse;
    }
}
