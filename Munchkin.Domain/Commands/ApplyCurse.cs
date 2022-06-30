using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Domain.Validation;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class ApplyCurse
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

                var place = game.Table.Places.FirstOrDefault(x => x.Character.Id == request.CharacterId);
                var combatField = game.Table.CombatField;

                if (place is null)
                {
                    return ValidationError.NoCharacter;
                }

                if (combatField.CursePlace is null)
                {
                    return ValidationError.NoCurse;
                }

                if (!place.Character.Curses.Any(x => x.Id == combatField.CursePlace.Id))
                {
                    return ValidationError.UncursedCharacted;
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
                var @event = new CharacterAppliedCurseEvent(request.GameId, request.CharacterId);

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return new Response();
            }
        }

        public record Response : CqrsResponse;
    }
}
