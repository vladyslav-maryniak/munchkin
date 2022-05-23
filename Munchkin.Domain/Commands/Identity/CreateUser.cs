using MediatR;
using Microsoft.AspNetCore.Identity;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Identity;

namespace Munchkin.Domain.Commands.Identity
{
    public static class CreateUser
    {
        public record Command(string Nickname, string Email, string Password) : IRequest<Response>;

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly UserManager<ApplicationUser> userManager;
            private readonly IGameRepository repository;

            public Handler(
                UserManager<ApplicationUser> userManager,
                IGameRepository repository)
            {
                this.userManager = userManager;
                this.repository = repository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new ApplicationUser(request.Nickname, request.Email);
                var result = await userManager.CreateAsync(user, request.Password);

                _ = await repository.CreatePlayerAsync(user.Id, request.Nickname, cancellationToken);

                return new Response(result);
            }
        }

        public record Response(IdentityResult Result);
    }
}
