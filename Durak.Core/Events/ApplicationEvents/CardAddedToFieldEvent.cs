using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Players;

namespace Durak.Core.Events.ApplicationEvents;

public class CardAddedToFieldEvent : BaseApplicationEvent
{
	public readonly GameCard Card;
	public CardAddedToFieldEvent(int player,GameCard card) : base(player) =>
		Card = card;
}
