using Durak.Core.Events;
using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Shared;

namespace Durak.Core.GameModels.CardSets;

public class PlayerHand : BaseEntity<int>
{
		public int PlayerId { get; set; }

		protected HashSet<Card> _cards;


		public void AddCard(PlacedCard playedCard)
		{
			var card = playedCard.Card;
			if (_cards.Contains(card))
				throw new AggregateException("Card already exists!");

			_cards.Add(card);

			Events.Add(new CardDrawnToHandEvent());
		}

		public PlacedCard DrawCard(PlacedCard playedCard)
		{
			var card = playedCard.Card;

			if (!_cards.Contains(card))
				throw new AggregateException("This card cannot be drawn");

			_cards.Remove(card);
			Events.Add(new CardDrawnFromHandEvent());

			return playedCard;
		}

}
