using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Domain.Queries;
using Munchkin.Domain.Validation;
using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Events;
using Munchkin.Shared.Events.Base;

namespace Munchkin.Domain.Commands
{
    public static class DrawCard
    {
        public record Command(Guid GameId, Guid PlayerId) : IRequest<Response>;

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

                if (!game.Table.Places.Any(x => x.Player.Id == request.PlayerId))
                {
                    return ValidationError.NoPlayer;
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

                var card = game.Table.PeekDoorCard(out bool shuffled);
                if (shuffled)
                {
                    var innerEvent = new DoorDeckRanOutEvent(game.Id, game.Table.DoorDeck.Select(x => x.Id).ToList());
                    await mediator.Send(new PublishEvent.Command(innerEvent), cancellationToken);
                }

                IGameEvent @event = card switch
                {
                    MonsterCard => new MonsterCardDrewEvent(request.GameId, request.PlayerId),
                    CurseCard => new CurseCardDrewEvent(request.GameId, request.PlayerId),
                    MonsterEnhancerCard => new MonsterEnhancerCardDrewEvent(request.GameId, request.PlayerId),
                    _ => throw new NotImplementedException(),
                };

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return new Response();
            }
        }

        public record Response : CqrsResponse;
    }
}
