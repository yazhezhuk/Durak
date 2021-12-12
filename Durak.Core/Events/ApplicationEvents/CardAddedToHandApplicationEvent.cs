using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Players;

namespace Durak.Core.Events.ApplicationEvents;

public class CardAddedToHandApplicationEvent : BaseApplicationEvent
{
	public Card Card { get; }

	public CardAddedToHandApplicationEvent(Player actionClaimant,Card card) : base(actionClaimant)
	{
		Card = card;
	}

	public CardAddedToHandApplicationEvent(int id, Card card) : base(id)
	{
		Card = card;
	}
}
