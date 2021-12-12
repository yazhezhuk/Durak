using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Players;

namespace Durak.Core.Events.ApplicationEvents;

public class CardRemovedFromField : BaseApplicationEvent
{
	public Card Card { get; }

	public CardRemovedFromField(Player actionClaimant, Card card) : base(actionClaimant)
	{
		Card = card;
	}

	public CardRemovedFromField(int id, Card card) : base(id)
	{
		Card = card;
	}
}
