using Durak.Core.Events.IntegrationEvents;
using Durak.Core.GameModels.Cards;

namespace Durak.Core.Events.ApplicationEvents;

public class PlaceCardApplicationEvent : BaseApplicationEvent
{
	public readonly GameCard Card;

	public PlaceCardApplicationEvent(GameCard card) =>
		Card = card;
}
