using System.Collections.Generic;
using Durak.Core.Events;

namespace Durak.Core.Game
{
	public abstract class BaseEntity<T>
	{
		public T Id { get; set; }
		public List<BaseEvent> Events { get; set; }
	}
}
