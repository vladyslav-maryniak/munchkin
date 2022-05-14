using MediatR;
using Microsoft.AspNetCore.Identity;
using Munchkin.Shared.Identity;
using System.Security.Claims;

namespace Munchkin.Domain.Commands.Identity
{
    public static class GetUser
    {
        public record Query(ClaimsPrincipal User) : IRequest<Response>;

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly UserManager<ApplicationUser> userManager;

            public Handler(UserManager<ApplicationUser> userManager)
            {
                this.userManager = userManager;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByNameAsync(request.User.Identity?.Name);

                if (user == null)
                {
                    return new Response(Guid.Empty, string.Empty);
                }

                return new Response(user.Id, user.UserName);
            }
        }

        public record Response(Guid Id, string Nickname);
    }
}
