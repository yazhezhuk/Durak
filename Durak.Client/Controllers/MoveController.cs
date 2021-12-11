using System.Security.Claims;
using Durak.Client.Models;
using Durak.Client.Services;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using Durak.Core.Services;
using Durak.Infrastructure.Integration;
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
	private readonly IIntegrationEventPublisher _eventPublisher;
	private readonly IGameSessionService _gameSessionService;
	private readonly UserManager<AppUser> _userManager;
	private readonly IGameSessionRepository _gameSessionRepository;
	private readonly IFieldValidator _fieldValidator;
	private readonly IMoveService _moveService;

	private readonly AppUser _currentUser;
	private readonly GameSession _currentGameSession;



	public MoveController(
		IMoveService moveService,
		IRepository<Game> gameRepository,
		IGameSessionRepository gameSessionRepository,
		IGameSessionService gameSessionService, UserManager<AppUser> userManager, IIntegrationEventPublisher eventPublisher)
	{
		_moveService = moveService;
		_gameSessionRepository = gameSessionRepository;
		_gameSessionService = gameSessionService;
		_userManager = userManager;
		_eventPublisher = eventPublisher;

		var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
		_currentUser =_userManager.FindByNameAsync(currentUserName).Result;
	}

	[HttpPost("attack")]
	public IActionResult Attack([FromBody] AttackModel attackModel)
	{
		var gameSession = _gameSessionRepository.GetByGameName(attackModel.GameName);
		var game = gameSession.Game;

		if (game.ValidateUserHaveRightRole(_currentUser, Role.Attacker)
		{

		}

		_moveService.PlaceCard(game,attackModel.Card,game.AttackPlayer);

		return Ok(new { game });
	}

	[HttpPost("defend")]
	public IActionResult Defend([FromBody] DefendModel defendModel)
	{

		var gameSession = _gameSessionRepository.GetByUserName(_currentUser.UserName);
		var game = gameSession.Game;


		_moveService.PlaceCard(game,attackModel.Card,game.AttackPlayer);

		return Ok(new { game });
	}


}


