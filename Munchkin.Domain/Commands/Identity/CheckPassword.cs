using MediatR;
using Microsoft.AspNetCore.Identity;
using Munchkin.Shared.Extensions;
using Munchkin.Shared.Identity;

namespace Munchkin.Domain.Commands.Identity
{
    public static class CheckPassword
    {
        public record Command(string Nickname, string Email, string Password) : IRequest<Response>;

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly UserManager<ApplicationUser> userManager;

            public Handler(UserManager<ApplicationUser> userManager)
            {
                this.userManager = userManager;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByNameOrEmailAsync(request.Nickname, request.Email);
                if (user is null)
                {
                    return new Response(CanSignIn: false);
                }

                var result = await userManager.CheckPasswordAsync(user, request.Password);
                return new Response(CanSignIn: result);
            }
        }

        public record Response(bool CanSignIn);
    }
}
