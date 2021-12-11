using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.CardSets;
using Durak.Core.GameModels.Session;
using Durak.Core.GameModels.Shared;
using Durak.Core.Interfaces;
using Newtonsoft.Json;

namespace Durak.Core.GameModels.Players;

public class Player : BaseEntity<int>, IRootEntity
{
	public Role CurrentRole { get; set; }
	public string AppUserId { get; set; }
	public AppUser AppUser { get; set; }
	public int GameId { get; set; }

	public Game Game { get; set; }

	public Player(int gameId, string appUserId)
	{
		AppUserId = appUserId;
		GameId = gameId;
	}

	public PlayerHand PlayerHand { get; set; }

	public void TakeEnoughCards(Deck deck)
	{
		var cardsNeeded = Helper.Game.HAND_SIZE - PlayerHand.CardsInHandCount;
		for (int i = 0; i < cardsNeeded; i++)
		{
			var card = deck.DrawRandomCard();
			PlayerHand.AddCard(card);
		}
	}
}
