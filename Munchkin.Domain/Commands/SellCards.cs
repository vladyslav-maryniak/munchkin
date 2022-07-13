using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Domain.Validation;
using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class SellCards
    {
        public record Command(Guid GameId, Guid PlayerId, IEnumerable<Guid> CardIds) : IRequest<Response>;

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

                var place = game.Table.Places.FirstOrDefault(x => x.Player.Id == request.PlayerId);

                if (place is null)
                {
                    return ValidationError.NoPlayer;
                }

                if (place.Character.Level > 8)
                {
                    return ValidationError.TooHighLevel;
                }

                var saleableCards = place.InHandCards
                    .Where(x => request.CardIds.Contains(x.Id))
                    .Cast<SaleableCard>();

                if (!saleableCards.Any())
                {
                    return ValidationError.NoSaleableCards;
                }

                if (request.CardIds.Any(cardId => !place.InHandCards.Any(x => x.Id == cardId)))
                {
                    return ValidationError.NoCard;
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
                var @event = new PlayerSoldCardsEvent(request.GameId, request.PlayerId, request.CardIds);

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return new Response();
            }
        }

        public record Response : CqrsResponse;
    }
}
