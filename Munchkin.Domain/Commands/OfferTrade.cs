using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Domain.Validation;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class OfferTrade
    {
        public record Command(
            Guid GameId,
            Guid OfferorId,
            Guid OffereeId,
            Guid[] OfferorItemCardIds,
            Guid[] OffereeItemCardIds) : IRequest<Response>;

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

                if (!places.Any(x => x.Player.Id == request.OfferorId)
                    || !places.Any(x => x.Player.Id == request.OffereeId))
                {
                    return ValidationError.NoPlayer;
                }

                var offeror = places.First(x => x.Player.Id == request.OfferorId);
                var offeree = places.First(x => x.Player.Id == request.OffereeId);

                if (!request.OfferorItemCardIds.Any(cardId
                        => offeror.Character.Equipment.ToEnumerable().Any(x => x.Id == cardId)) ||
                    !request.OffereeItemCardIds.Any(cardId
                        => offeree.Character.Equipment.ToEnumerable().Any(x => x.Id == cardId)))
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
                var @event = new PlayerOfferedTradeEvent(
                    request.GameId,
                    request.OfferorId,
                    request.OffereeId,
                    request.OfferorItemCardIds,
                    request.OffereeItemCardIds);

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return new Response();
            }
        }

        public record Response : CqrsResponse;
    }
}
