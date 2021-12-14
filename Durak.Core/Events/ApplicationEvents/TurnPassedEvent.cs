using Durak.Core.GameModels.Players;

namespace Durak.Core.Events.ApplicationEvents;

public class TurnPassedEvent : BaseApplicationEvent
{
	public Player TurnReceiver { get; }

	public TurnPassedEvent(Player actionClaimant, Player turnReceiver) : base(actionClaimant)
	{
		TurnReceiver = turnReceiver;
	}
}
