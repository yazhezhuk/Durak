using Durak.Core.Events;
using Durak.Core.GameModels.Shared;

namespace Durak.Core.GameModels.Cards;

public record Card(Lear Lear,Rank Rank) : ValueObject
{

	public BaseEvent BeatAnother(PlacedCard enemyCard, bool isEnemyCardPrime)
	{
		var isYourCardIsPrime = isEnemyCardPrime && Lear == enemyCard.Card.Lear;

		if (isEnemyCardPrime &&
		    (isEnemyCardPrime != isYourCardIsPrime || enemyCard.Card.Rank > Rank))
			return new BadCardEvent();

		return new BeatOpponentCardEvent(this,enemyCard.Card);
	}
}
