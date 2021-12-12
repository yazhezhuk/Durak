using Durak.Core.Events.ApplicationEvents;
using Durak.Core.Events.IntegrationEvents;
using Durak.Core.GameModels;
using Durak.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Durak.Core.Events.EventHandlers;

public class CardTakenEventHandler : BaseEventHandler<CardTakenApplicationEvent>
{
	public CardTakenEventHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{ }

	public override Task Handle(CardTakenApplicationEvent notification, CancellationToken cancellationToken)
	{
		Logger.LogInformation("Card:{} added to hand", notification.Card);
		return EventPublisher.PublishEvent(new RemoveCardIntegrationEvent(notification.Card));
	}
}
