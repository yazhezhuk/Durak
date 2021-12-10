using MediatR;

namespace Durak.Core.Events.IntegrationEvents;

public abstract class BaseEvent : INotification
{
	public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
}
