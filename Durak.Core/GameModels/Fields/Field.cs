using Durak.Core.Events;
using Durak.Core.Events.ApplicationEvents;
using Durak.Core.Events.IntegrationEvents;
using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.CardSets;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Shared;
using Durak.Core.Interfaces;

namespace Durak.Core.GameModels.Fields;

public class Field : BaseEntity<int>, IRootEntity
{
	public int GameId { get; set; }

	public Field(int gameId)
	{
		GameId = gameId;
	}

	public List<GameCard> PlayedCards { get; set; } = new List<GameCard>();

	public List<GameCard> ListPlacedCards(Player player) =>
		PlayedCards
			.Where(playerCard => playerCard.PlayerId != player.Id)
			.ToList();

	public void RemoveAllCards()
	{
		foreach (var playedCard in PlayedCards)
		{
			PlayedCards.Remove(playedCard);
		}
	}


	public void RemoveCard(GameCard card)
	{
		if (PlayedCards.Count > 0 && PlayedCards.Any(c => c.Id == card.Id) &&
		    PlayedCards.Any(c => c.Card == card.Card))
			throw new ArgumentException("Card inst on the field",nameof(card));

		PlayedCards.Remove(card);
		Events.Add(new CardRemovedFromField(card.PlayerId,card.Card));
	}

	public void PlayCard(GameCard card)
	{
		PlayedCards.Add(card);
		Events.Add(new PlaceCardApplicationEvent(card.PlayerId,card));
	}

	public void PlayCardToDefend(GameCard playerCard,GameCard enemyCard)
	{
		PlayedCards.Add(playerCard);
		//Events.Add(new CardDefeatedIntegrationEvent(playerCard,enemyCard));
	}

}
