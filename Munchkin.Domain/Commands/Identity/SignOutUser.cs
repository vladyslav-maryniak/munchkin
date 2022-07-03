using MediatR;
using Microsoft.AspNetCore.Identity;
using Munchkin.Shared.Identity;

namespace Munchkin.Domain.Commands.Identity
{
    public static class SignOutUser
    {
        public record Command : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly SignInManager<ApplicationUser> signInManager;

            public Handler(SignInManager<ApplicationUser> signInManager)
            {
                this.signInManager = signInManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await signInManager.SignOutAsync();

                return Unit.Value;
            }
        }
    }
}
