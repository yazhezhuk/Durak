using Durak.Core.GameModels.Players;
using MediatR;

namespace Durak.Core.Events.ApplicationEvents;

public abstract class BaseApplicationEvent : INotification
{
	public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
	public int ActionClaimantId { get; protected set; }

	protected BaseApplicationEvent(Player actionClaimant)
	{
		ActionClaimantId = actionClaimant.Id;
	}
	protected BaseApplicationEvent(int id)
	{
		ActionClaimantId = id;
	}
}
