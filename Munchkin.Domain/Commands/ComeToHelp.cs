using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Domain.Validation;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class ComeToHelp
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

                var places = game.Table.Places;
                var characterSquad = game.Table.CombatField.CharacterSquad;

                if (!places.Any(x => x.Character.Id == request.CharacterId))
                {
                    return ValidationError.NoCharacter;
                }

                if (characterSquad.Any(x => x.Id == request.CharacterId))
                {
                    return ValidationError.CharacterOnCombatField;
                }

                if (characterSquad.Count < 1)
                {
                    return ValidationError.NoCharacterNeedsHelp;
                }

                if (characterSquad.Count > 1)
                {
                    return ValidationError.AnotherCharacterCameToHelp;
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
                var @event = new CharacterGotHelpEvent(request.GameId, request.CharacterId);

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return new Response();
            }
        }

        public record Response : CqrsResponse;
    }
}
