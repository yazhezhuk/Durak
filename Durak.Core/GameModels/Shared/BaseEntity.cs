using Durak.Core.Events;

namespace Durak.Core.GameModels.Shared;
public abstract class BaseEntity<T>
{
	public T Id { get; set; }
	public List<BaseEvent> Events { get; set; }
}
