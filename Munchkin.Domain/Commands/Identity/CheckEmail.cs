using MediatR;
using Microsoft.AspNetCore.Identity;
using Munchkin.Shared.Extensions;
using Munchkin.Shared.Identity;

namespace Munchkin.Domain.Commands.Identity
{
    public static class CheckEmail
    {
        public record Command(string Email) : IRequest<Response>;

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly UserManager<ApplicationUser> userManager;

            public Handler(UserManager<ApplicationUser> userManager)
            {
                this.userManager = userManager;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByEmailAsync(request.Email);
                
                return new Response(IsUnique: user is null);
            }
        }

        public record Response(bool IsUnique);
    }
}
