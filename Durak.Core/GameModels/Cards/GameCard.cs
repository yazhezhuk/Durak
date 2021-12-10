using Durak.Core.GameModels.Shared;
using Durak.Core.Interfaces;

namespace Durak.Core.GameModels.Cards;

public class GameCard : BaseEntity<int>, IRootEntity
{
	public Card Card { get; set; }
	public int? ConnectedPlayerId { get; set; }
	public int GameId { get; set; }

	public GameCard(int gameId,Card card, int connectedPlayerId)
	{
		Card = card;
		ConnectedPlayerId = connectedPlayerId;
		GameId = gameId;
	}
	public GameCard(int gameId,Card card)
	{
		Card = card;
		GameId = gameId;
	}
	private GameCard()
	{

	}
}
