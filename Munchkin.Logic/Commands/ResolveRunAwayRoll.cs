using MediatR;
using Munchkin.DataAccess.Base;
using Munchkin.Infrastucture.Events;
using Munchkin.Infrastucture.Events.Base;

namespace Munchkin.Logic.Commands
{
    public static class ResolveRunAwayRoll
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

                IGameEvent @event = game.Table.DieValue > 4 ?
                    new PlayerEscapedEvent(request.GameId, request.PlayerId) :
                    new PlayerAppliedBadStuffEvent(request.GameId, request.PlayerId);

                await service.PublishAsync(@event);

                return Unit.Value;
            }
        }
    }
}
