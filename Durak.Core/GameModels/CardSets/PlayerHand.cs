using Durak.Core.Events;
using Durak.Core.Events.ApplicationEvents;
using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Shared;
using Durak.Core.Interfaces;
using Newtonsoft.Json;

namespace Durak.Core.GameModels.CardSets;

public class PlayerHand : BaseEntity<int>,IRootEntity
{
	public HashSet<GameCard> Cards { get; set; }

	public int PlayerId { get; set; }

	public PlayerHand(int playerId)
	{
		PlayerId = playerId;
		Cards = new HashSet<GameCard>();
	}

	public int CardsInHandCount => Cards.Count;

	public void AddCard(GameCard card)
	{
		if (Cards.Count > 0 && Cards.Any(c => c.Id == card.Id) &&
		    Cards.Any(c => c.Card == card.Card))
			throw new AggregateException("Card already exists!");

		Cards.Add(card);

		Events.Add(new CardAddedToHandApplicationEvent(PlayerId,card.Card));
	}

	public string ToJson()
	{
		return JsonConvert.SerializeObject(Cards);
	}

	public GameCard DrawCard(Card card)
	{
		var matchingCard = Cards.FirstOrDefault(c => c.Card == card);

		if (matchingCard == null)
			throw new AggregateException("Card cant be found in the hand.");

		Cards.Remove(matchingCard);

		Events.Add(new CardDrawnFromHandApplicationEvent(PlayerId,matchingCard.Card));
		return matchingCard;
	}

}
