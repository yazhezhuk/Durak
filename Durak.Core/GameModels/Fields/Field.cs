using Durak.Core.Events;
using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.CardSets;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Shared;
using Durak.Core.Interfaces;

namespace Durak.Core.GameModels.Fields;

public class Field : BaseEntity<int>, IRootEntity
{
	public int GameId { get; set; }
	public int DeckId { get; set; }

	public Field(int deckId)
	{
		DeckId = deckId;
	}

	public Deck Deck { get; set; }
	public virtual List<PlacedCard> PlayedCards { get; set; }


	public List<Card> ListCardsByPlayer(Player player)
	{
		return PlayedCards
			.Where(playerCard => playerCard.PlayerId != player.Id)
			.Select(playedCard => playedCard.Card)
			.ToList();
	}

	public void RemoveAllCards()
	{
		foreach (var playedCard in PlayedCards)
		{
			PlayedCards.Remove(playedCard);
		}
	}

	public void RemoveCard(PlacedCard card)
	{
		PlayedCards.Remove(card);
		Events.Add(new RemoveCardEvent());
	}

	public void PlayCard(PlacedCard card)
	{

		PlayedCards.Add(card);
		Events.Add(new PlaceCardEvent());
	}

}
