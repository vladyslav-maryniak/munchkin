using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public class LeavePlayer
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
                PlayerLeftEvent @event = new(request.GameId, request.PlayerId);
                await service.PublishAsync(@event);

                return Unit.Value;
            }
        }
    }
}
