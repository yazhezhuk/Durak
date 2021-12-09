using Core.Infrastructure;
using Core.Infrastructure.Integration;
using Durak.Core.GameModels;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
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

	public GameController(IRepository<Game> gameRepository,GameHub gameHub)
	{
		_gameHub = gameHub;
		_gameRepository = gameRepository;
	}

	[HttpGet("create")]
	public IActionResult Create()
	{


	}
}