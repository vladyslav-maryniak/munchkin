using MediatR;
using Microsoft.AspNetCore.Identity;
using Munchkin.Shared.Identity;
using System.Security.Claims;

namespace Munchkin.Domain.Commands.Identity
{
    public static class CheckSignIn
    {
        public record Command(ClaimsPrincipal Principal) : IRequest<Response>;

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly SignInManager<ApplicationUser> signInManager;

            public Handler(SignInManager<ApplicationUser> signInManager)
            {
                this.signInManager = signInManager;
            }

            public Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = signInManager.IsSignedIn(request.Principal);

                return Task.FromResult(new Response(result));
            }
        }

        public record Response(bool Result);
    }
}
