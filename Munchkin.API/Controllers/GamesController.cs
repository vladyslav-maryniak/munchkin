using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Munchkin.API.DTOs;
using Munchkin.Logic.Commands;

namespace Munchkin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public GamesController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost("join")]
        public async Task<ActionResult> JoinPlayerAsync(JoinPlayerDto dto)
        {
            var command = mapper.Map<JoinPlayer.Command>(dto);
            await mediator.Send(command);
            
            return Ok();
        }
    }
}
