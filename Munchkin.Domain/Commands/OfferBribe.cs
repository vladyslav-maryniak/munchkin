using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Domain.Validation;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class OfferBribe
    {
        public record Command(
            Guid GameId,
            Guid OfferorId,
            Guid OffereeId,
            string Agreement,
            Guid[] EncourageItemCardIds) : IRequest<Response>;

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

                if (request.EncourageItemCardIds.Any(cardId => !offeror.InHandCards.Any(x => x.Id == cardId)))
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
                var @event = new PlayerOfferedBribeEvent(
                    request.GameId,
                    request.OfferorId,
                    request.OffereeId,
                    request.Agreement,
                    request.EncourageItemCardIds);

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return new Response();
            }

        }
        
        public record Response : CqrsResponse;
    }
}
