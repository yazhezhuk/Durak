using MediatR;

namespace Durak.Core.Events.ApplicationEvents;

public class BaseApplicationEvent : INotification
{
	public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
}
