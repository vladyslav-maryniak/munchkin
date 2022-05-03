using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Events.Base;

namespace Munchkin.Domain.Commands
{
    public static class PublishEvent
    {
        public record Command(IGameEvent Event) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IEventService service;

            public Handler(IEventService service)
            {
                this.service = service;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await service.PublishAsync(request.Event);

                return Unit.Value;
            }
        }
    }
}
