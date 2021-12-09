using Durak.Core.Events;
using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Shared;
using Durak.Core.Interfaces;

namespace Durak.Core.GameModels.CardSets;

public class Deck : BaseEntity<int>, IRootEntity
{
	private HashSet<Card> _cards;


	public void SetDeckCards(List<Card> cards)
	{
		if (cards.Equals(null) || !cards.Any())
			throw new ArgumentException("Cannot set deck cards", nameof(cards));

		_cards = cards.ToHashSet();
	}
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

	public void Fill()
	{
		foreach (var lear in Enum.GetValues(typeof(Lear)).Cast<Lear>())
		{
			foreach (var rank in Enum.GetValues(typeof(Rank)).Cast<Rank>())
			{
				var card = new Card(lear, rank);
				AddCard(card);
			}
		}
	}


}
