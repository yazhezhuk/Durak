using Durak.Core.Events.ApplicationEvents;
using Durak.Core.Events.IntegrationEvents;
using Durak.Core.Interfaces;
using MediatR;

namespace Durak.Core.Events.EventHandlers;

public class RemoveCardEventHandler : BaseEventHandler<RemoveCardApplicationEvent>
{

	public RemoveCardEventHandler(IIntegrationEventPublisher eventPublisher) : base(eventPublisher)
	{ }

	public override Task Handle(RemoveCardApplicationEvent notification, CancellationToken cancellationToken)
	{
		return _eventPublisher.PublishEvent(new RemoveCardIntegrationEvent(notification.Card));
	}


}
