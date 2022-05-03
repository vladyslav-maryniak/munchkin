using MediatR;
using Munchkin.DataAccess.Base;
using Munchkin.Infrastucture.Events;

namespace Munchkin.Logic.Commands
{
    public static class JoinPlayer
    {
        public record Command(Guid GameId, Guid PlayerId) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IGameRepository repository;
            private readonly IEventService service;

            public Handler(IGameRepository repository, IEventService service)
            {
                this.repository = repository;
                this.service = service;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var player = await repository.GetPlayerAsync(request.PlayerId);
                var @event = new PlayerJoinedEvent(request.GameId, player);

                await service.PublishAsync(@event);

                return Unit.Value;
            }
        }
    }
}
