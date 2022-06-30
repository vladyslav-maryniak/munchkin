using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Domain.Validation;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class AcceptOffer
    {
        public record Command(Guid GameId, Guid OfferId) : IRequest<Response>;

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

                if (!game.Table.Offers.Any(x => x.Id == request.OfferId))
                {
                    return ValidationError.NoOffer;
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
                var @event = new PlayerAcceptedOfferEvent(request.GameId, request.OfferId);

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return new Response();
            }
        }
        
        public record Response : CqrsResponse;
    }
}
