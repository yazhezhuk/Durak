using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Players;

namespace Durak.Core.Events.ApplicationEvents;

public class CardDrawnFromHandApplicationEvent : BaseApplicationEvent
{
	public readonly Card Card;


	public CardDrawnFromHandApplicationEvent(int actionClaimant, Card card) : base(actionClaimant)
	{
		Card = card;
	}
}
