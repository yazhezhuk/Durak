using Microsoft.AspNetCore.SignalR;

namespace Durak.Core.Events.IntegrationEvents;

public abstract class BaseIntegrationEvent
{
	public string Name => GetType().Name;

	public abstract Task Publish(IHubClients hubClients);

}
