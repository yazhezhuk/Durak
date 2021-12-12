using Durak.Core.GameModels.Session;
using Microsoft.AspNetCore.SignalR;

namespace Durak.Core.Events.IntegrationEvents;

public class TurnEndIntegrationEvent : BaseIntegrationEvent
{
	private readonly Game _game;

	public TurnEndIntegrationEvent(Game game)
	{
		_game = game;
	}

	public override Task Publish(IHubClients hubClients) =>
		hubClients.All.SendAsync(Name, _game);

}
