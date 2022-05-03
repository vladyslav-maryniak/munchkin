using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Events;
using Munchkin.Shared.Events.Base;

namespace Munchkin.Domain.Commands
{
    public static class ResolveRunAwayRoll
    {
        public record Command(Guid GameId, Guid CharacterId) : IRequest;

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
                    new CharacterEscapedEvent(request.GameId, request.CharacterId) :
                    new CharacterAppliedBadStuffEvent(request.GameId, request.CharacterId);

                await service.PublishAsync(@event);

                return Unit.Value;
            }
        }
    }
}
