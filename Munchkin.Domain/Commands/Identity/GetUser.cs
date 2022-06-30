using MediatR;
using Microsoft.AspNetCore.Identity;
using Munchkin.Domain.Validation;
using Munchkin.Shared.Identity;
using System.Security.Claims;

namespace Munchkin.Domain.Commands.Identity
{
    public static class GetUser
    {
        public record Query(ClaimsPrincipal User) : IRequest<Response>;

        public class Validator : IValidationHandler<Query>
        {
            private readonly UserManager<ApplicationUser> userManager;

            public Validator(UserManager<ApplicationUser> userManager)
            {
                this.userManager = userManager;
            }

            public async Task<ValidationResult> Validate(Query request)
            {
                var user = await userManager.FindByNameAsync(request.User.Identity?.Name);

                if (user is null)
                {
                    return ValidationError.UnregisteredPlayer;
                }

                return ValidationResult.Success;
            }
        }

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

                return new Response() { Id = user.Id, Nickname = user.UserName };
            }
        }

        public record Response : CqrsResponse
        {
            public Guid Id { get; set; }
            public string? Nickname { get; set; }
        }
    }
}
