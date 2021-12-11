using Durak.Core.Events.IntegrationEvents;
using Durak.Core.GameModels.Cards;

namespace Durak.Core.Events.ApplicationEvents;

public class RemoveCardApplicationEvent : BaseApplicationEvent
{
	public readonly GameCard Card;

	public RemoveCardApplicationEvent(GameCard card)
	{
		Card = card;
	}
}
