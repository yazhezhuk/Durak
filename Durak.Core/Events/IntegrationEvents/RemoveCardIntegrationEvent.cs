
using Durak.Core.GameModels.Cards;
using Microsoft.AspNetCore.SignalR;

namespace Durak.Core.Events.IntegrationEvents;

public class RemoveCardIntegrationEvent : BaseIntegrationEvent
{
	private readonly GameCard _card;

	public RemoveCardIntegrationEvent(GameCard card)
	{
		_card = card;
	}

	public override Task Publish(IHubClients hubClients) =>
		hubClients.All.SendAsync(Name, _card);
	}
