using System;
using Durak.Core.Events;

namespace Durak.Core.Game
{
	public record Card : ValueObject
	{
		public Lear Lear { get; }
		public Rank Rank { get; }

		public Card(Lear lear,Rank rank)
		{
			Lear = lear;
			Rank = rank;
		}

		public BaseEvent BeatAnother(PlacedCard enemyCard, bool isEnemyCardPrime)
		{
			var isYourCardIsPrime = false;
			if (isEnemyCardPrime) isYourCardIsPrime = Lear == enemyCard.Card.Lear;

			if (isEnemyCardPrime &&
			    (isEnemyCardPrime != isYourCardIsPrime || enemyCard.Card.Rank > Rank))
				return new BadCardEvent();

			return new BeatOpponentCardEvent(this,enemyCard.Card);
		}
	}
}
