using AutoMapper;
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
        private readonly IMapper mapper;

        public IdentityController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<ActionResult<IdentityResultDto>> CreateUserAsync(
            CreateUserDto dto, CancellationToken cancellationToken)
        {
            var command = new CreateUser.Command(dto.Nickname, dto.Email, dto.Password);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response.Result is null ?
                response : mapper.Map<IdentityResultDto>(response.Result));
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<ActionResult<SignInResultDto>> SignInUserAsync(
            SignInDto dto, CancellationToken cancellationToken)
        {
            var command = new SignInUser.Command(dto.Nickname, dto.Email, dto.Password, dto.IsPersistent);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response.Result is null ? response : mapper.Map<SignInResultDto>(response.Result));
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
        public async Task<ActionResult<CheckSignInResultDto>> CheckSignInAsync(CancellationToken cancellationToken)
        {
            var command = new CheckSignIn.Command(User);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(new CheckSignInResultDto { Authenticated = response.Result });
        }

        [HttpGet("user")]
        public async Task<ActionResult<UserDto>> GetUserAsync(CancellationToken cancellationToken)
        {
            var command = new GetUser.Query(User);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response.Nickname is null ?
                response : new UserDto { Id = response.Id, Nickname = response.Nickname });
        }

        [AllowAnonymous]
        [HttpPost("check-nickname")]
        public async Task<ActionResult<CheckNicknameResultDto>> CheckNicknameAsync(
            CheckNicknameDto dto, CancellationToken cancellationToken)
        {
            var command = new CheckNickname.Command(dto.Nickname);
            var response = await mediator.Send(command, cancellationToken);
            
            return Ok(new CheckNicknameResultDto { IsUnique = response.IsUnique });
        }

        [AllowAnonymous]
        [HttpPost("check-email")]
        public async Task<ActionResult<CheckEmailResultDto>> CheckEmailAsync(
            CheckEmailDto dto, CancellationToken cancellationToken)
        {
            var command = new CheckEmail.Command(dto.Email);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(new CheckEmailResultDto { IsUnique = response.IsUnique });
        }

        [AllowAnonymous]
        [HttpPost("check-password")]
        public async Task<ActionResult<CheckPasswordResultDto>> CheckPasswordAsync(
            CheckPasswordDto dto, CancellationToken cancellationToken)
        {
            var command = new CheckPassword.Command(dto.Nickname, dto.Email, dto.Password);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(new CheckPasswordResultDto { CanSignIn = response.CanSignIn });
        }
    }
}
