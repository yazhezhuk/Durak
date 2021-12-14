using System.Security.Claims;
using Durak.Client.Models;
using Durak.Client.Services;
using Durak.Core.Events.EventHandlers;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using Durak.Core.Services;
using Durak.Infrastructure.Integration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Durak.Client.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MoveController : ControllerBase
{
	private readonly UserManager<AppUser> _userManager;
	private readonly IGameSessionRepository _gameSessionRepository;
	private readonly IFieldValidator _fieldValidator;
	private readonly IMediator _mediator;
	private readonly IMoveService _moveService;
	private readonly IRepository<Game> _gameRepository;
	private readonly GameSession _currentGameSession;



	public MoveController(IMediator mediator,
		IMoveService moveService,
		IRepository<Player> playerRepository,
		IRepository<Game> gameRepository,
		IGameSessionRepository gameSessionRepository,
		IGameSessionService gameSessionService, UserManager<AppUser> userManager)
	{
		_mediator = mediator;
		_moveService = moveService;
		_gameRepository = gameRepository;
		_gameSessionRepository = gameSessionRepository;
		_userManager = userManager;


	}

	[HttpPost("attack")]
	public IActionResult Attack([FromBody] AttackModel attackModel)
	{
		var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
		var currentUser =_userManager.FindByNameAsync(currentUserName).Result;

		var gameSession = _gameSessionRepository.Get(attackModel.GameId);
		var game = gameSession.Game;

		if (!game.ValidateUserCanMove(currentUser, Role.Attacker))
		{
			//_eventPublisher.PublishEvent(new InvalidActionIntegrationEvent());
			return Problem("invalid action!");
		}

		_moveService.PlaceCard(game,attackModel.Card,game.AttackPlayer);

		return Ok(new { game });
	}

	[HttpPost("defend")]
	public IActionResult Defend([FromBody] DefendModel defendModel)
	{

		var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
		var currentUser =_userManager.FindByNameAsync(currentUserName).Result;

		var gameSession = _gameSessionRepository.GetByUserName(currentUser.UserName);
		var game = gameSession.Game;


		if (!game.ValidateUserCanMove(currentUser, Role.Defender))
		{
			//_eventPublisher.PublishEvent(new InvalidActionIntegrationEvent());
			return Problem("invalid action!");
		}

		_moveService.DefendFromCard(game,defendModel.PlayerCard,defendModel.EnemyCard);

		return Ok(new { game });
	}

	[HttpPost("pass")]
	public IActionResult Pass([FromQuery]int gameId)
	{
		var currentUser = ResolveCurrentUserFromClaims();
		var currentGame = _gameRepository.Get(gameId);
		var userWithGameIdentity = currentUser.AsGameIdentity(currentGame.Id);

		if (!currentGame.ValidateUserCanMove(currentUser, Role.Both))
		{
			_mediator.Publish(
				new InvalidOperationEvent(userWithGameIdentity,
					"Cannot pass turn, please wait for your turn."));
			return Problem("invalid action!");
		}

		currentGame.PassMove();

		return Ok();
	}

	private AppUser ResolveCurrentUserFromClaims()
	{
		var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
		return _userManager.FindByNameAsync(currentUserName).Result;
	}


}
