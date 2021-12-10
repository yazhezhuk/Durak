using Durak.Core.Events;
using Durak.Core.Events.IntegrationEvents;

namespace Durak.Core.GameModels.Shared;
public abstract class BaseEntity<T>
{
	public T Id { get; set; }
	public List<BaseEvent> Events = new List<BaseEvent>();
}
