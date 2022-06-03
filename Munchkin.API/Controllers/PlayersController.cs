using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Munchkin.API.DTOs;
using Munchkin.Domain.Queries;

namespace Munchkin.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/players")]
    public class PlayersController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public PlayersController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet("{playerId:guid}/games")]
        public async Task<ActionResult<List<PlayerGameDto>>> GetPlayerGamesAsync(
            Guid playerId, CancellationToken cancellationToken)
        {
            var query = new GetPlayerGames.Query(playerId);
            var response = await mediator.Send(query, cancellationToken);

            return Ok(mapper.Map<List<PlayerGameDto>>(response.Games));
        }
    }
}
