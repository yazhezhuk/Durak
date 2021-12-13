using Durak.Core.GameModels.Session;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Durak.Core.Events.IntegrationEvents;

public class StartGameIntegrationEvent : BaseIntegrationEvent
{
	private readonly Game _game;
	public StartGameIntegrationEvent(Game game)
	{
		_game = game;
	}

	public override Task Publish(IHubClients hubClients) =>
		hubClients.All.SendAsync(Name, JsonConvert.SerializeObject(_game));
}
