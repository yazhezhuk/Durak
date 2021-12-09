using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Core.Infrastructure.Integration;

public class GameHub : Hub
{
	private readonly ILogger _logger;
	public GameHub(ILogger<GameHub> logger)
	{
		_logger = logger;
		_logger.LogInformation("Hub created");
	}

	public override Task OnConnectedAsync()
	{
		_logger.LogInformation("Hub connection established.");
		return base.OnConnectedAsync();
	}


	public async Task Send(string someShite)
	{
		_logger.LogInformation("Sending message");
		await Clients.All.SendAsync("ReceiveMethod", someShite);
	}
}
