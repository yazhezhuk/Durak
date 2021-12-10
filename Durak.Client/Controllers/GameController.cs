using Durak.Client.Models;
using Durak.Infrastructure;
using Durak.Core.GameModels;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using Durak.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Durak.Client.Controllers;

[Authorize]
[ApiController]
[Route("api/")]
public class GameController : ControllerBase
{
	private readonly IRepository<Game> _gameRepository;
	private readonly GameHub _gameHub;
	private readonly IGameSessionService _gameSessionService;

	public GameController(
		IRepository<Game> gameRepository,
		GameHub gameHub,
		IGameSessionService gameSessionService)
	{
		_gameHub = gameHub;
		_gameRepository = gameRepository;
		_gameSessionService = gameSessionService;
	}

	[HttpPost("create")]
	public IActionResult Create([FromBody] GameModel gameModel)
	{
		var game = _gameSessionService.CreateEmptyGame(gameModel.Name);
		return Ok(new
		{
			game
		});
	}
}
