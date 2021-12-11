using Durak.Core.GameModels.Session;
using Microsoft.AspNetCore.SignalR;

namespace Durak.Core.Events.IntegrationEvents;

public class StartGameIntegrationEvent : BaseIntegrationEvent
{
	private readonly Game _game;
	public StartGameIntegrationEvent(Game game)
	{
		_game = game;
	}

	public override Task Publish(Hub hub) =>
		hub.Clients.All.SendAsync(Name, _game);
}
