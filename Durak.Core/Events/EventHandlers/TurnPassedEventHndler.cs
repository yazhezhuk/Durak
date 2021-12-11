using Durak.Core.Events.ApplicationEvents;
using Durak.Core.Events.IntegrationEvents;
using Durak.Core.Interfaces;
using MediatR;

namespace Durak.Core.Events.EventHandlers;

public class TurnPassedEventHandler : BaseEventHandler<TurnPassedEvent>
{
	public override Task Handle(TurnPassedEvent notification, CancellationToken cancellationToken)
	{
		return _eventPublisher.PublishEvent(new TurnPassedIntegrationEvent());
	}

	public TurnPassedEventHandler(IIntegrationEventPublisher eventPublisher) : base(eventPublisher)
	{ }
}
