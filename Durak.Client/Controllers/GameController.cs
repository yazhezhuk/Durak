using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Durak.Client.Models;
using Durak.Client.Services;
using Durak.Infrastructure;
using Durak.Core.GameModels;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using Durak.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Durak.Client.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
	private readonly IRepository<Game> _gameRepository;
	private readonly IGameSessionService _gameSessionService;
	private readonly UserManager<AppUser> _userManager;
	private readonly IGameSessionRepository _gameSessionRepository;

	public GameController(
		IRepository<Game> gameRepository,
		IGameSessionRepository gameSessionRepository,
		IGameSessionService gameSessionService, UserManager<AppUser> userManager)
	{
		_gameSessionRepository = gameSessionRepository;
		_gameRepository = gameRepository;
		_gameSessionService = gameSessionService;
		_userManager = userManager;
	}

	[HttpPost("create")]
	public IActionResult Create([FromBody] GameModel gameModel)
	{
		var game = _gameSessionService.CreateEmptyGame(gameModel.Name);
		return Ok(new { game });
	}

	[HttpGet("all")]
	public List<GameSessionModel> GetAll()
	{
		var game = _gameSessionRepository.GetAll();


		return game.Select(g => new GameSessionModel
			{
				FirstUser = new UserModel
				{
					Email = g.Game.AttackPlayer?.AppUser.Email ?? "",
					Name = g.Game.AttackPlayer?.AppUser.UserName ?? ""
				},
				SecondUser = new UserModel
				{
					Email = g.Game.DefencePlayer?.AppUser.Email ?? "",
					Name = g.Game.DefencePlayer?.AppUser.UserName ?? ""
				},
				Name = g.Game.Name

			})
			.ToList();
	}

	[HttpPost("connect")]
	public IActionResult Connect([FromBody] GameModel gameModel)
	{
		var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
		AppUser appUser = _userManager.FindByNameAsync(currentUserName).Result;

		var gameSession = _gameSessionRepository.GetByGameName(gameModel.Name);

		var result =  _gameSessionService.HandlePlayerConnection(appUser,gameSession);


		return Ok(new
		{
			result
		});
	}
}
