using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Munchkin.API.DTOs.Identity;
using Munchkin.Domain.Commands.Identity;

namespace Munchkin.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/identity")]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator mediator;

        public IdentityController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<ActionResult<bool>> CreateUserAsync(CreateUserDto dto, CancellationToken cancellationToken)
        {
            var command = new CreateUser.Command(dto.Nickname, dto.Email, dto.Password);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response.Result.Succeeded);
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<ActionResult<bool>> SignInUserAsync(SignInUserDto dto, CancellationToken cancellationToken)
        {
            var command = new SignInUser.Command(dto.Nickname, dto.Email, dto.Password, dto.IsPersistent);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response.Result.Succeeded);
        }

        [HttpPost("sign-out")]
        public async Task<ActionResult> SignOutUserAsync(CancellationToken cancellationToken)
        {
            var command = new SignOutUser.Command();
            _ = await mediator.Send(command, cancellationToken);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("check-sign-in")]
        public async Task<ActionResult<bool>> CheckSignInAsync(CancellationToken cancellationToken)
        {
            var command = new CheckSignIn.Command(User);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response.Result);
        }

        [HttpGet("user")]
        public async Task<ActionResult<UserDto>> GetUserAsync(CancellationToken cancellationToken)
        {
            var command = new GetUser.Query(User);
            var response = await mediator.Send(command, cancellationToken);

            var dto = new UserDto { Id = response.Id, Nickname = response.Nickname };
            
            return Ok(dto);
        }
    }
}
