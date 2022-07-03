using MediatR;
using Microsoft.AspNetCore.Identity;
using Munchkin.Shared.Identity;

namespace Munchkin.Domain.Commands.Identity
{
    public static class CheckNickname
    {
        public record Command(string Nickname) : IRequest<Response>;

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly UserManager<ApplicationUser> userManager;

            public Handler(UserManager<ApplicationUser> userManager)
            {
                this.userManager = userManager;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByNameAsync(request.Nickname);

                return new Response(IsUnique: user is null);
            }
        }

        public record Response(bool IsUnique);
    }
}
