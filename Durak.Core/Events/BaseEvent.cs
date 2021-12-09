using System;
using MediatR;

namespace Durak.Core.Events;

public abstract class BaseEvent : INotification
{
	public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
}
