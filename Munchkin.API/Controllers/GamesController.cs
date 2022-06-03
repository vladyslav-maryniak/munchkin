using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Munchkin.API.DTOs;
using Munchkin.Domain.Commands;
using Munchkin.Domain.Queries;

namespace Munchkin.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/games")]
    public class GamesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public GamesController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateGameAsync(CancellationToken cancellationToken)
        {
            var command = new CreateGame.Command();
            var response = await mediator.Send(command, cancellationToken);

            return Ok(mapper.Map<CreatedGameDto>(response.Game));
        }

        [HttpGet("{gameId:guid}")]
        public async Task<ActionResult<GameDto>> GetGameAsync(
            Guid gameId, CancellationToken cancellationToken)
        {
            var query = new GetGame.Query(gameId);
            var response = await mediator.Send(query, cancellationToken);

            return Ok(mapper.Map<GameDto>(response.Game));
        }

        [HttpGet("{gameId:guid}/lobby")]
        public async Task<ActionResult<GameLobbyDto>> GetGameLobbyAsync(
            Guid gameId, CancellationToken cancellationToken)
        {
            var query = new GetGameLobby.Query(gameId);
            var response = await mediator.Send(query, cancellationToken);

            return Ok(mapper.Map<GameLobbyDto>(response.Lobby));
        }

        [HttpPost("{gameId:guid}/update-state")]
        public async Task<ActionResult> UpdateGameStateAsync(
            Guid gameId, GameStateDto dto, CancellationToken cancellationToken)
        {
            var command = new UpdateGameState.Command(gameId, dto.State);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("{gameId:guid}/join")]
        public async Task<ActionResult> JoinPlayerAsync(
            Guid gameId, PlayerEventDto dto, CancellationToken cancellationToken)
        {
            var command = new JoinPlayer.Command(gameId, dto.PlayerId);
            await mediator.Send(command, cancellationToken);
            
            return Ok();
        }

        [HttpPost("{gameId:guid}/leave")]
        public async Task<ActionResult> LeavePlayerAsync(
            Guid gameId, PlayerEventDto dto, CancellationToken cancellationToken)
        {
            var command = new LeavePlayer.Command(gameId, dto.PlayerId);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("{gameId:guid}/start-game")]
        public async Task<ActionResult> StartGameAsync(
            Guid gameId, CancellationToken cancellationToken)
        {
            var command = new StartGame.Command(gameId);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("{gameId:guid}/draw-card")]
        public async Task<ActionResult> DrawCardAsync(
            Guid gameId, PlayerEventDto dto, CancellationToken cancellationToken)
        {
            var command = new DrawCard.Command(gameId, dto.PlayerId);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("{gameId:guid}/apply-curse")]
        public async Task<ActionResult> ApplyCurseAsync(
            Guid gameId, CharacterEventDto dto, CancellationToken cancellationToken)
        {
            var command = new ApplyCurse.Command(gameId, dto.CharacterId);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("{gameId:guid}/initiate-combat")]
        public async Task<ActionResult> InitiateCombatAsync(
            Guid gameId, CharacterEventDto dto, CancellationToken cancellationToken)
        {
            var command = new InitiateCombat.Command(gameId, dto.CharacterId);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("{gameId:guid}/complete-combat")]
        public async Task<ActionResult> CompleteCombatAsync(
            Guid gameId, CancellationToken cancellationToken)
        {
            var command = new CompleteCombat.Command(gameId);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("{gameId:guid}/roll-die")]
        public async Task<ActionResult> RollDieAsync(
            Guid gameId, PlayerEventDto dto, CancellationToken cancellationToken)
        {
            var command = new RollDie.Command(gameId, dto.PlayerId);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("{gameId:guid}/resolve-run-away-roll")]
        public async Task<ActionResult> ResolveRunAwayAsync(
            Guid gameId, CharacterEventDto dto, CancellationToken cancellationToken)
        {
            var command = new ResolveRunAwayRoll.Command(gameId, dto.CharacterId);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("{gameId:guid}/come-to-help")]
        public async Task<ActionResult> ComeToHelpAsync(
            Guid gameId, CharacterEventDto dto, CancellationToken cancellationToken)
        {
            var command = new ComeToHelp.Command(gameId, dto.CharacterId);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("{gameId:guid}/stop-asking-for-help")]
        public async Task<ActionResult> StopAskingForHelpAsync(
            Guid gameId, CancellationToken cancellationToken)
        {
            var command = new StopAskingForHelp.Command(gameId);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("{gameId:guid}/resume-combat")]
        public async Task<ActionResult> ResumeGameAsync(
            Guid gameId, CharacterEventDto dto, CancellationToken cancellationToken)
        {
            var command = new ResumeCombat.Command(gameId, dto.CharacterId);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("{gameId:guid}/play-card")]
        public async Task<ActionResult> PlayCardAsync(
            Guid gameId, PlayCardEventDto dto, CancellationToken cancellationToken)
        {
            var command = new PlayCard.Command(gameId, dto.PlayerId, dto.CardId, dto.Metadata);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("{gameId:guid}/sell-cards")]
        public async Task<ActionResult> SellCardsAsync(
            Guid gameId, CardSaleEventDto dto, CancellationToken cancellationToken)
        {
            var command = new SellCards.Command(gameId, dto.PlayerId, dto.CardIds);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
