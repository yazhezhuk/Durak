using Durak.Core.Events.ApplicationEvents;
using Newtonsoft.Json;

namespace Durak.Core.GameModels.Shared;
public abstract class BaseEntity<T>
{
	public T Id { get; set; }
	[JsonIgnore]
	public List<BaseApplicationEvent> Events = new List<BaseApplicationEvent>();
}
