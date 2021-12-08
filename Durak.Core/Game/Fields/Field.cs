using System.Collections.Generic;
using System.Linq;
using Durak.Core.Events;
using Durak.Core.Game;
using Durak.Core.Interfaces;

namespace Durak.Core.Game.Fields;

public class Field : BaseEntity<int>, IRootEntity
{
	public int DeckId { get; set; }
	public Deck Deck { get; set; }

	public Game Game;

	public List<PlacedCard> PlayedCards { get; set; }


	public List<Card> GetCardsPlayedByPlayer(Player player)
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
