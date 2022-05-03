using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class RollDie
    {
        public record Command(Guid GameId, Guid PlayerId) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IEventService service;

            public Handler(IEventService service)
            {
                this.service = service;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Random r = new();
                var dieValue = r.Next(1, 7);
                var @event = new PlayerRolledDieEvent(request.GameId, request.PlayerId, dieValue);

                await service.PublishAsync(@event);

                return Unit.Value;
            }
        }
    }
}
