using System;
using System.Collections.Generic;
using System.Linq;
using Durak.Core.Events;
using Durak.Core.Interfaces;
using MediatR;

namespace Durak.Core.Game
{
	public class Deck : BaseEntity<int>, IRootEntity
	{
		protected HashSet<Card> _cards;

		public int GameId { get; set; }

		public void DrawCard(Card card)
		{
			if (!_cards.Contains(card))
				throw new ArgumentException("Card cannot be found!");

			_cards.Remove(card);
			Events.Add(new CardDrawnToHandEvent());
		}


		public void AddCard(Card card)
		{
			if (_cards.Contains(card))
				throw new AggregateException("Card already exists!");

			_cards.Add(card);
		}

		public Card DrawRandomCard()
		{
			if (!_cards.Any())
				throw new InvalidOperationException("Deck is empty!");

			var randomNum = Random.Shared.Next(1, _cards.Count);
			var drawnCard = _cards.Skip(randomNum).First();

			DrawCard(drawnCard);

			return drawnCard;
		}

	}
}
