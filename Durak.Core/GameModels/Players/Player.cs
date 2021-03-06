using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.CardSets;
using Durak.Core.GameModels.Session;
using Durak.Core.GameModels.Shared;
using Durak.Core.Interfaces;
using Newtonsoft.Json;

namespace Durak.Core.GameModels.Players;

public class Player : BaseEntity<int>, IRootEntity
{
	public bool CanMove { get; set; }
	public Role CurrentRole { get; set; }
	public string AppUserId { get; set; }
	[JsonIgnore]
	public AppUser AppIdentity { get; set; }
	public int GameId { get; set; }

	[JsonIgnore]
	public Game Game { get; set; }

	public Player(int gameId, string appUserId)
	{
		AppUserId = appUserId;
		GameId = gameId;
	}

	public static readonly Player None  =  new Player(0, "");
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
