using Durak.Core.Events;
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
		if (Cards.Contains(card))
			throw new AggregateException("Card already exists!");

		Cards.Add(card);

		Events.Add(new CardDrawnToHandEvent());
	}

	public string ToJson()
	{
		return JsonConvert.SerializeObject(Cards);
	}

	public GameCard DrawCard(GameCard playedCard)
	{

		if (!Cards.Contains(playedCard))
			throw new AggregateException("This card cannot be drawn");

		Cards.Remove(playedCard);
		Events.Add(new CardDrawnFromHandEvent());

		return playedCard;
	}

}
