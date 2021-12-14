using Durak.Core.Events.ApplicationEvents;
using Durak.Core.GameModels.Players;

namespace Durak.Core.Events.EventHandlers;

public class InvalidOperationEvent : BaseApplicationEvent
{
	public string ErrorMessage { get; }


	public InvalidOperationEvent(Player actionClaimant,string errorMessage) : base(actionClaimant)
	{
		ErrorMessage = errorMessage;
	}
}
