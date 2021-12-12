using Durak.Core.Events.IntegrationEvents;
using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Players;

namespace Durak.Core.Events.ApplicationEvents;

public class CardTakenApplicationEvent : BaseApplicationEvent
{
	public readonly GameCard Card;

	public CardTakenApplicationEvent(Player player,GameCard card) : base(player)
	{
		Card = card;
	}
}
