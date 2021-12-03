using System;
using System.Collections.Generic;

namespace Durak.Core.Game
{
	public class Deck : BaseEntity
	{
		private List<Card> _cards;

		public Deck()
		{

		}

		public Card DrewNextCard()
		{
			var randomNum = Random.Shared.Next(1, _cards.Count);
			var drawnCard = _cards[randomNum];

			_cards.Remove(drawnCard);

			return drawnCard;
		}

	}
}
