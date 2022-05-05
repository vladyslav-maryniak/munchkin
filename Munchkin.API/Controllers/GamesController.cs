using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Munchkin.API.DTOs;
using Munchkin.Domain.Commands;
using Munchkin.Domain.Queries;

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
        public async Task<ActionResult> JoinPlayerAsync(Guid gameId, PlayerEventDto dto)
        {
            var command = new JoinPlayer.Command(gameId, dto.PlayerId);
            await mediator.Send(command);
            
            return Ok();
        }

        [HttpPost("{gameId:guid}/leave")]
        public async Task<ActionResult> LeavePlayerAsync(Guid gameId, PlayerEventDto dto)
        {
            var command = new LeavePlayer.Command(gameId, dto.PlayerId);
            await mediator.Send(command);

            return Ok();
        }

        [HttpPost("{gameId:guid}/start-game")]
        public async Task<ActionResult> StartGameAsync(Guid gameId)
        {
            var command = new StartGame.Command(gameId);
            await mediator.Send(command);

            return Ok();
        }

        [HttpPost("{gameId:guid}/draw-card")]
        public async Task<ActionResult> DrawCardAsync(Guid gameId, PlayerEventDto dto)
        {
            var command = new DrawCard.Command(gameId, dto.PlayerId);
            await mediator.Send(command);

            return Ok();
        }

        [HttpPost("{gameId:guid}/initiate-combat")]
        public async Task<ActionResult> InitiateCombatAsync(Guid gameId, CharacterEventDto dto)
        {
            var command = new InitiateCombat.Command(gameId, dto.CharacterId);
            await mediator.Send(command);

            return Ok();
        }

        [HttpPost("{gameId:guid}/roll-die")]
        public async Task<ActionResult> RollDieAsync(Guid gameId, PlayerEventDto dto)
        {
            var command = new RollDie.Command(gameId, dto.PlayerId);
            await mediator.Send(command);

            return Ok();
        }

        [HttpPost("{gameId:guid}/resolve-run-away-roll")]
        public async Task<ActionResult> ResolveRunAwayAsync(Guid gameId, CharacterEventDto dto)
        {
            var command = new ResolveRunAwayRoll.Command(gameId, dto.CharacterId);
            await mediator.Send(command);

            return Ok();
        }

        [HttpPost("{gameId:guid}/come-to-help")]
        public async Task<ActionResult> ComeToHelpAsync(Guid gameId, CharacterEventDto dto)
        {
            var command = new ComeToHelp.Command(gameId, dto.CharacterId);
            await mediator.Send(command);

            return Ok();
        }

        [HttpPost("{gameId:guid}/resume-combat")]
        public async Task<ActionResult> ResumeGameAsync(Guid gameId, CharacterEventDto dto)
        {
            var command = new ResumeCombat.Command(gameId, dto.CharacterId);
            await mediator.Send(command);

            return Ok();
        }

        [HttpPost("{gameId:guid}/play-card")]
        public async Task<ActionResult> PlayCardAsync(Guid gameId, PlayCardEventDto dto)
        {
            var command = new PlayCard.Command(gameId, dto.PlayerId, dto.CardId);
            await mediator.Send(command);

            return Ok();
        }
    }
}
