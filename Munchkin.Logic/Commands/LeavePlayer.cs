using MediatR;
using Munchkin.DataAccess;
using Munchkin.Infrastucture.Events;
using Munchkin.Infrastucture.Projections;

namespace Munchkin.Logic.Commands
{
    public class LeavePlayer
    {
        public record Command(Guid GameId, Player Player) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IEventService service;

            public Handler(IEventService service)
            {
                this.service = service;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                PlayerLeftEvent @event = new(request.GameId, request.Player);
                await service.PublishAsync(@event);

                return Unit.Value;
            }
        }
    }
}
