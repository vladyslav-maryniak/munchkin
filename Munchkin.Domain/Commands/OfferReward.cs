using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Domain.Queries;
using Munchkin.Domain.Validation;
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
        bool HelperPicksFirst) : IRequest<Response>;

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

                var places = game.Table.Places;

                if (!places.Any(x => x.Player.Id == request.OfferorId))
                {
                    return ValidationError.NoPlayer;
                }

                var offeror = places.First(x => x.Player.Id == request.OfferorId);

                if (request.ItemCardIds.Any(cardId => !offeror.InHandCards.Any(x => x.Id == cardId))
                    || request.CardIdsForPlay.Any(cardId => !offeror.InHandCards.Any(x => x.Id == cardId)))
                {
                    return ValidationError.NoCard;
                }

                if (request.CardIdsForPlay.Any(x => request.ItemCardIds.Contains(x)))
                {
                    return ValidationError.CardDuplication;
                }

                var combatField = game.Table.CombatField;

                if (request.NumberOfTreasures < 0 || request.NumberOfTreasures > combatField.VictoryTreasures)
                {
                    return ValidationError.InvalidNumberOfTreasures;
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
                var combatField = game.Table.CombatField;

                var @event = new PlayerOfferedRewardEvent(
                    request.GameId,
                    request.OfferorId,
                    request.ItemCardIds,
                    request.CardIdsForPlay,
                    combatField.VictoryTreasures,
                    request.NumberOfTreasures,
                    request.HelperPicksFirst);

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return new Response();
            }
        }

        public record Response : CqrsResponse;
    }
}
