using Durak.Core.GameModels.Shared;

namespace Durak.Core.GameModels.Cards;

public class PlacedCard : BaseEntity<int>
{
	public int CardId { get; set; }
	public int ConnectedPlayerId { get; set; }
	public int GameId { get; set; }

	public PlacedCard(int gameId,int cardId, int connectedPlayerId)
	{
		CardId = cardId;
		ConnectedPlayerId = connectedPlayerId;
		GameId = gameId;
	}

	public Card Card { get; }
}
