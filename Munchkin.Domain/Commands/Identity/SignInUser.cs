using MediatR;
using Microsoft.AspNetCore.Identity;
using Munchkin.Domain.Validation;
using Munchkin.Shared.Extensions;
using Munchkin.Shared.Identity;

namespace Munchkin.Domain.Commands.Identity
{
    public static class SignInUser
    {
        public record Command(string Nickname, string Email, string Password, bool IsPersistent) : IRequest<Response>;

        public class Validator : IValidationHandler<Command>
        {
            private readonly UserManager<ApplicationUser> userManager;

            public Validator(UserManager<ApplicationUser> userManager)
            {
                this.userManager = userManager;
            }

            public async Task<ValidationResult> Validate(Command request)
            {
                var user = await userManager.FindByNameOrEmailAsync(request.Nickname, request.Email);

                if (user is null)
                {
                    return ValidationError.UnregisteredPlayer;
                }

                return ValidationResult.Success;
            }
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly UserManager<ApplicationUser> userManager;
            private readonly SignInManager<ApplicationUser> signInManager;

            public Handler(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager)
            {
                this.userManager = userManager;
                this.signInManager = signInManager;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByNameOrEmailAsync(request.Nickname, request.Email);

                var result = await signInManager.PasswordSignInAsync(
                    user,
                    request.Password,
                    request.IsPersistent,
                    lockoutOnFailure: false);

                return new Response() { Result = result };
            }
        }

        public record Response : CqrsResponse
        {
            public SignInResult? Result { get; set; }
        }
    }
}
