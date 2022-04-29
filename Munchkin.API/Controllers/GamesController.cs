using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Munchkin.API.DTOs;
using Munchkin.Logic.Commands;
using Munchkin.Logic.Queries;

namespace Munchkin.API.Controllers
{
    [Route("api/games")]
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

        [HttpGet("{gameId:guid}")]
        public async Task<ActionResult<GameDto>> GetGameAsync(Guid gameId)
        {
            var query = new GetGame.Query(gameId);
            var response = await mediator.Send(query);

            return Ok(mapper.Map<GameDto>(response.Game));
        }

        [HttpPost("{gameId:guid}/join")]
        public async Task<ActionResult> JoinPlayerAsync(Guid gameId, EventDto dto)
        {
            var command = new JoinPlayer.Command(gameId, dto.PlayerId);
            await mediator.Send(command);
            
            return Ok();
        }

        [HttpPost("{gameId:guid}/leave")]
        public async Task<ActionResult> LeavePlayerAsync(Guid gameId, EventDto dto)
        {
            var command = new LeavePlayer.Command(gameId, dto.PlayerId);
            await mediator.Send(command);

            return Ok();
        }

        [HttpPost("{gameId:guid}/draw-card")]
        public async Task<ActionResult> DrawCardAsync(Guid gameId, EventDto dto)
        {
            var command = new DrawCard.Command(gameId, dto.PlayerId);
            await mediator.Send(command);

            return Ok();
        }
    }
}
