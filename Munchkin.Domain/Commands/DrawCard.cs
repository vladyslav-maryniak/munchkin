using MediatR;
using Munchkin.Domain.Queries;
using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Events;
using Munchkin.Shared.Events.Base;

namespace Munchkin.Domain.Commands
{
    public static class DrawCard
    {
        public record Command(Guid GameId, Guid PlayerId) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IMediator mediator;

            public Handler(IMediator mediator)
            {
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = await mediator.Send(new GetGame.Query(request.GameId), cancellationToken);
                var game = response.Game;

                if (game.IsPlayerTurn(request.PlayerId) == false)
                {
                    return Unit.Value;
                }

                var card = game.Table.DoorDeck.Pop();
                IGameEvent @event = card switch
                {
                    MonsterCard monsterCard => new MonsterCardDrewEvent(request.GameId, request.PlayerId, monsterCard),
                    CurseCard curseCard => new CurseCardDrewEvent(request.GameId, request.PlayerId, curseCard),
                    _ => throw new NotImplementedException(),
                };

                await mediator.Send(new PublishEvent.Command(@event), cancellationToken);
                game.TurnIndex++;

                return Unit.Value;
            }
        }
    }
}
