using Durak.Core.GameModels.Players;

namespace Durak.Core.Events.ApplicationEvents;

public class ErrorApplicationEvent : BaseApplicationEvent
{

	public ErrorApplicationEvent(Player actionClaimant) : base(actionClaimant)
	{ }
}
