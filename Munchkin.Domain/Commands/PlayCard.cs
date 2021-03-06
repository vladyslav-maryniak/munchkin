using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Domain.Queries;
using Munchkin.Domain.Validation;
using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Cards.Base.Treasures;
using Munchkin.Shared.Events;
using Munchkin.Shared.Events.Base;

namespace Munchkin.Domain.Commands
{
    public static class PlayCard
    {
        public record Command(
            Guid GameId,
            Guid PlayerId,
            Guid CardId,
            Dictionary<string, string>? Metadata = default) : IRequest<Response>;

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

                if (!place.InHandCards.Any(x => x.Id == request.CardId))
                {
                    return ValidationError.NoCard;
                }

                var card = place.InHandCards.First(x => x.Id == request.CardId);
                var equipment = place.Character.Equipment;

                switch (card)
                {
                    case ItemCard itemCard:
                        if (equipment.Has(itemCard))
                        {
                            return ValidationError.ItemIsEquipped;
                        }
                        if (game.Table.CombatField.CharacterSquad.Any(x => x.Id == place.Character.Id))
                        {
                            return ValidationError.EquipmentDuringCombat;
                        }
                        break;

                    case GoUpLevelCard:
                        if (place.Character.Level > 8)
                        {
                            return ValidationError.TooHighLevel;
                        }
                        if (game.Table.CombatField.CharacterSquad.Any(x => x.Id == place.Character.Id))
                        {
                            return ValidationError.GoUpLevelDuringCombat;
                        }
                        break;

                    case MonsterCard:
                        if (game.State != "DangerousDecisionMakingState")
                        {
                            return ValidationError.UnexpectedCommand;
                        }
                        break;

                    case MonsterEnhancerCard:
                        if (!game.Table.CombatField.MonsterSquad.Any())
                        {
                            return ValidationError.EnhancementOnEmptyCombatField;
                        }
                        break;

                    default:
                        break;
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
                var place = game.Table.Places.First(x => x.Player.Id == request.PlayerId);
                var card = place.InHandCards.First(x => x.Id == request.CardId);

                IGameEvent @event = card switch
                {
                    ItemCard => new ItemCardPlayedEvent(
                        request.GameId, request.PlayerId, request.CardId),
                    OneShotCard => new OneShotCardPlayedEvent(
                        request.GameId, request.PlayerId, request.CardId, request.Metadata),
                    GoUpLevelCard => new GoUpLevelCardPlayedEvent(
                        request.GameId, request.PlayerId, request.CardId),
                    MonsterCard => new MonsterCardPlayedEvent(
                        request.GameId, request.PlayerId, request.CardId),
                    MonsterEnhancerCard => new MonsterEnhancerCardPlayedEvent(
                        request.GameId, request.PlayerId, request.CardId, request.Metadata),
                    CurseCard => new CurseCardPlayedEvent(
                        request.GameId, request.PlayerId, request.CardId, request.Metadata),
                    _ => throw new NotImplementedException()
                };

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);

                return new Response();
            }
        }

        public record Response : CqrsResponse;
    }
}
