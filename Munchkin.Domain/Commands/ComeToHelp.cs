using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Events;

namespace Munchkin.Domain.Commands
{
    public static class ComeToHelp
    {
        public record Command(Guid GameId, Guid CharacterId) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IEventService service;

            public Handler(IEventService service)
            {
                this.service = service;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var @event = new CharacterGotHelpEvent(request.GameId, request.CharacterId);

                await service.PublishAsync(@event);

                return Unit.Value;
            }
        }
    }
}
