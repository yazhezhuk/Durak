using Durak.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Durak.Core.Events.EventHandlers;

public abstract class BaseEventHandler<T> : INotificationHandler<T> where T : INotification
{
	protected IIntegrationEventPublisher _eventPublisher;

	protected BaseEventHandler(IIntegrationEventPublisher eventPublisher) =>
		_eventPublisher = eventPublisher;

	public abstract Task Handle(T notification, CancellationToken cancellationToken);
}
