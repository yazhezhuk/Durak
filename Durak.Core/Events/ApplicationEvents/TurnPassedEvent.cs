using Durak.Core.GameModels.Players;

namespace Durak.Core.Events.ApplicationEvents;

public class TurnPassedEvent : BaseApplicationEvent
{
	public TurnPassedEvent(Player actionClaimant) : base(actionClaimant)
	{ }
}
