using MediatR;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Events;
using Munchkin.Shared.Models;

namespace Munchkin.Domain.Commands
{
    public static class CreateGame
    {
        public record Command : IRequest<Response>;

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IMediator mediator;
            private readonly IGameRepository repository;

            public Handler(IMediator mediator, IGameRepository repository)
            {
                this.mediator = mediator;
                this.repository = repository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var game = await repository.CreateGameAsync(cancellationToken);
                var @event = new GameCreatedEvent(game.Id);

                await mediator.Send(new PublishEvent.Command(@event));

                return new Response(game);
            }
        }

        public record Response(Game Game);
    }
}
