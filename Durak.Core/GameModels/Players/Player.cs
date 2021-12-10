using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.CardSets;
using Durak.Core.GameModels.Session;
using Durak.Core.GameModels.Shared;
using Durak.Core.Interfaces;

namespace Durak.Core.GameModels.Players;

public class Player : BaseEntity<int>, IRootEntity
{
	public CurrentRole CurrentRole { get; set; }
	public string UserId { get; set; }
	public int GameId { get; set; }
	public Game Game { get; set; }

	public Player(int gameId, string userId)
	{
		UserId = userId;
		GameId = gameId;
	}

	public PlayerHand PlayerHand { get; }

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
