using Durak.Core.GameModels.CardSets;
using MediatR;

namespace Durak.Core.Events.IntegrationEvents;

public class StartGameEvent : INotification
{
	public PlayerHand Hand;
	public StartGameEvent(PlayerHand playerHand)
	{
		Hand = playerHand;
	}
}
