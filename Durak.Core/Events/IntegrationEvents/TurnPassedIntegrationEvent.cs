using Microsoft.AspNetCore.SignalR;

namespace Durak.Core.Events.IntegrationEvents;

public class TurnPassedIntegrationEvent : BaseIntegrationEvent
{

	public override Task Publish(IHubClients hubClients)
	{
		return null;
	}
}
