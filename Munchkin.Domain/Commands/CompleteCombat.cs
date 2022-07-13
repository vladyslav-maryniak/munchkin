using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Domain.Queries;
using Munchkin.Domain.Validation;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class CompleteCombat
    {
        public record Command(Guid GameId) : IRequest<Response>;

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

                return ValidationResult.Success;
            }
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IMediator mediator;
            private const int maxCharacterLevel = 10;

            public Handler(IMediator mediator)
            {
                this.mediator = mediator;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = await mediator.Send(new GetGame.Query(request.GameId), cancellationToken);

                var @event = new CombatCompletedEvent(request.GameId);
                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                var game = response.Game!;
                var places = game.Table.Places;

                if (places.Any(x => x.Character.Level >= maxCharacterLevel))
                {
                    var winner = places.MaxBy(x => x.Character.Level)!;

                    var victoryEvent = new PlayerWonGameEvent(game.Id, winner.Player.Id);
                    await mediator.Send(new PublishEvent.Command(victoryEvent), cancellationToken);

                    return new Response();
                }

                var combatField = game.Table.CombatField;
                var reward = combatField.Reward;

                if (reward is not null && combatField.CharacterSquadStrength > combatField.MonsterSquadStrength)
                {
                    foreach (var cardId in reward.CardIdsForPlay)
                    {
                        await mediator.Send(new PlayCard.Command(request.GameId, reward.OffereeId, cardId));
                    }
                }

                return new Response();
            }
        }

        public record Response : CqrsResponse;
    }
}
