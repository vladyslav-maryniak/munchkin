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

                return new Response();
            }
        }

        public record Response : CqrsResponse;
    }
}
