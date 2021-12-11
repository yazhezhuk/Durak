using Durak.Core.Events;
using Durak.Core.Events.ApplicationEvents;
using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Shared;
using Durak.Core.Interfaces;
using Newtonsoft.Json;

namespace Durak.Core.GameModels.CardSets;

public class Deck : BaseEntity<int>, IRootEntity
{
	public int GameId { get; set; }

	public Deck(int gameId)
	{
		GameId = gameId;
	}

	public HashSet<GameCard> Cards { get; set; } = new HashSet<GameCard>();

	public void SetDeckCards(List<GameCard> cards)
	{
		if (cards.Equals(null) || !cards.Any())
			throw new ArgumentException("Cannot set deck cards", nameof(cards));

		Cards = cards.ToHashSet();
	}
	public void DrawCard(GameCard card)
	{
		if (!Cards.Contains(card))
			throw new ArgumentException("Card cannot be found!");

		Cards.Remove(card);
		Events.Add(new CardDrawnToHandApplicationEvent());
	}


	public void AddCard(GameCard card)
	{
		if (Cards.Contains(card))
			throw new AggregateException("Card already exists!");

		Cards.Add(card);
	}

	public GameCard DrawRandomCard()
	{
		if (!Cards.Any())
			throw new InvalidOperationException("Deck is empty!");

		var randomNum = Random.Shared.Next(1, Cards.Count);
		var drawnCard = Cards.Skip(randomNum).First();

		DrawCard(drawnCard);

		return drawnCard;
	}

	public void Fill()
	{
		foreach (var lear in Enum.GetValues(typeof(Lear)).Cast<Lear>())
		{
			foreach (var rank in Enum.GetValues(typeof(Rank)).Cast<Rank>())
			{
				var card = new GameCard(GameId,new Card(lear,rank));
				AddCard(card);
			}
		}
	}


}
