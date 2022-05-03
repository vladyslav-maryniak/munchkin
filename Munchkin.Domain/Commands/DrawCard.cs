using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Events;
using Munchkin.Shared.Events.Base;

namespace Munchkin.Domain.Commands
{
    public static class DrawCard
    {
        public record Command(Guid GameId, Guid PlayerId) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IEventService service;
            private readonly IGameRepository repository;

            public Handler(IEventService service, IGameRepository repository)
            {
                this.service = service;
                this.repository = repository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var game = await repository.GetGameAsync(request.GameId);
                var card = game.DoorDeck.Pop();

                IGameEvent @event = card switch
                {
                    MonsterCard monsterCard => new MonsterCardDrewEvent(request.GameId, request.PlayerId, monsterCard),
                    CurseCard curseCard => new CurseCardDrewEvent(request.GameId, request.PlayerId, curseCard),
                    _ => throw new NotImplementedException(),
                };

                await service.PublishAsync(@event);

                return Unit.Value;
            }
        }
    }
}
