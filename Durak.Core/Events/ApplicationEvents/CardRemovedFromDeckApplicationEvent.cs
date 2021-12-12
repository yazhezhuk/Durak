using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.CardSets;
using Durak.Core.GameModels.Players;

namespace Durak.Core.Events.ApplicationEvents;

public class CardRemovedFromDeckApplicationEvent : BaseApplicationEvent
{
	public Card RemovedCard { get; }

	public CardRemovedFromDeckApplicationEvent(Card removedCard, Player player) : base(player)
	{
		RemovedCard = removedCard;
	}
}
