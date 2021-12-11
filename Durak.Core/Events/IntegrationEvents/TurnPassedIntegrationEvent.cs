using Microsoft.AspNetCore.SignalR;

namespace Durak.Core.Events.IntegrationEvents;

public class TurnPassedIntegrationEvent : BaseIntegrationEvent
{

	public override Task Publish(Hub hub)
	{
		return hub.Clients.Others.SendAsync(Name);
	}
}
