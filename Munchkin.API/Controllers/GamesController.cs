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

            return Ok(response.Game is null ? response : mapper.Map<GameDto>(response.Game));
        }

        [HttpGet("{gameId:guid}/lobby")]
        public async Task<ActionResult<GameLobbyDto>> GetGameLobbyAsync(
            Guid gameId, CancellationToken cancellationToken)
        {
            var query = new GetGameLobby.Query(gameId);
            var response = await mediator.Send(query, cancellationToken);

            return Ok(response.Lobby is null ? response : mapper.Map<GameLobbyDto>(response.Lobby));
        }

        [HttpPost("{gameId:guid}/update-state")]
        public async Task<ActionResult> UpdateGameStateAsync(
            Guid gameId, GameStateDto dto, CancellationToken cancellationToken)
        {
            var command = new UpdateGameState.Command(gameId, dto.State);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/join")]
        public async Task<ActionResult> JoinPlayerAsync(
            Guid gameId, PlayerEventDto dto, CancellationToken cancellationToken)
        {
            var command = new JoinPlayer.Command(gameId, dto.PlayerId);
            var response = await mediator.Send(command, cancellationToken);
            
            return Ok(response);
        }

        [HttpPost("{gameId:guid}/leave")]
        public async Task<ActionResult> LeavePlayerAsync(
            Guid gameId, PlayerEventDto dto, CancellationToken cancellationToken)
        {
            var command = new LeavePlayer.Command(gameId, dto.PlayerId);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/start-game")]
        public async Task<ActionResult> StartGameAsync(
            Guid gameId, CancellationToken cancellationToken)
        {
            var command = new StartGame.Command(gameId);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/draw-card")]
        public async Task<ActionResult> DrawCardAsync(
            Guid gameId, PlayerEventDto dto, CancellationToken cancellationToken)
        {
            var command = new DrawCard.Command(gameId, dto.PlayerId);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/apply-curse")]
        public async Task<ActionResult> ApplyCurseAsync(
            Guid gameId, CharacterEventDto dto, CancellationToken cancellationToken)
        {
            var command = new ApplyCurse.Command(gameId, dto.CharacterId);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/initiate-combat")]
        public async Task<ActionResult> InitiateCombatAsync(
            Guid gameId, CharacterEventDto dto, CancellationToken cancellationToken)
        {
            var command = new InitiateCombat.Command(gameId, dto.CharacterId);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/complete-combat")]
        public async Task<ActionResult> CompleteCombatAsync(
            Guid gameId, CancellationToken cancellationToken)
        {
            var command = new CompleteCombat.Command(gameId);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/roll-die")]
        public async Task<ActionResult> RollDieAsync(
            Guid gameId, PlayerEventDto dto, CancellationToken cancellationToken)
        {
            var command = new RollDie.Command(gameId, dto.PlayerId);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/resolve-run-away-roll")]
        public async Task<ActionResult> ResolveRunAwayAsync(
            Guid gameId, CharacterEventDto dto, CancellationToken cancellationToken)
        {
            var command = new ResolveRunAwayRoll.Command(gameId, dto.CharacterId);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/come-to-help")]
        public async Task<ActionResult> ComeToHelpAsync(
            Guid gameId, CharacterEventDto dto, CancellationToken cancellationToken)
        {
            var command = new ComeToHelp.Command(gameId, dto.CharacterId);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/stop-asking-for-help")]
        public async Task<ActionResult> StopAskingForHelpAsync(
            Guid gameId, CancellationToken cancellationToken)
        {
            var command = new StopAskingForHelp.Command(gameId);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/resume-combat")]
        public async Task<ActionResult> ResumeGameAsync(
            Guid gameId, CharacterEventDto dto, CancellationToken cancellationToken)
        {
            var command = new ResumeCombat.Command(gameId, dto.CharacterId);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/play-card")]
        public async Task<ActionResult> PlayCardAsync(
            Guid gameId, PlayCardEventDto dto, CancellationToken cancellationToken)
        {
            var command = new PlayCard.Command(gameId, dto.PlayerId, dto.CardId, dto.Metadata);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/sell-cards")]
        public async Task<ActionResult> SellCardsAsync(
            Guid gameId, CardSaleEventDto dto, CancellationToken cancellationToken)
        {
            var command = new SellCards.Command(gameId, dto.PlayerId, dto.CardIds);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/offer-reward")]
        public async Task<ActionResult> OfferRewardAsync(
            Guid gameId, RewardDto dto, CancellationToken cancellationToken)
        {
            var command = new OfferReward.Command(
                gameId,
                dto.OfferorId,
                dto.ItemCardIds,
                dto.CardIdsForPlay,
                dto.NumberOfTreasures,
                dto.HelperPicksFirst);

            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/offer-bribe")]
        public async Task<ActionResult> OfferBribeAsync(
            Guid gameId, BribeDto dto, CancellationToken cancellationToken)
        {
            var command = new OfferBribe.Command(
                gameId,
                dto.OfferorId,
                dto.OffereeId,
                dto.Agreement,
                dto.ItemCardIds);

            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/offer-trade")]
        public async Task<ActionResult> OfferTradeAsync(
            Guid gameId, TradeDto dto, CancellationToken cancellationToken)
        {
            var command = new OfferTrade.Command(
                gameId,
                dto.OfferorId,
                dto.OffereeId,
                dto.OfferorItemCardIds,
                dto.OffereeItemCardIds);

            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/accept-offer")]
        public async Task<ActionResult> AcceptOfferAsync(
            Guid gameId, OfferDesicionDto dto, CancellationToken cancellationToken)
        {
            var command = new AcceptOffer.Command(gameId, dto.OfferId);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{gameId:guid}/decline-offer")]
        public async Task<ActionResult> DeclineOfferAsync(
            Guid gameId, OfferDesicionDto dto, CancellationToken cancellationToken)
        {
            var command = new DeclineOffer.Command(gameId, dto.OfferId);
            var response = await mediator.Send(command, cancellationToken);

            return Ok(response);
        }
    }
}
