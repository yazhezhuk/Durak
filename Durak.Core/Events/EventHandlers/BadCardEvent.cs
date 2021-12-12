using Durak.Core.Events.ApplicationEvents;
using Durak.Core.GameModels.Players;

namespace Durak.Core.Events.EventHandlers;

public class BadCardApplicationEvent : BaseApplicationEvent
{

	public BadCardApplicationEvent(Player actionClaimant) : base(actionClaimant)
	{ }
}
