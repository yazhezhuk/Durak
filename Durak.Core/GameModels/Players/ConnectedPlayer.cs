using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Shared;

namespace Durak.Core.GameModels.Players;

public class ConnectedPlayer : BaseEntity<int>
{
	public ConnectedPlayer(int gameSessionId, string playerId)
	{
		PlayerId = playerId;
		GameSessionId = gameSessionId;
		CardsInHand = new List<Card>();
	}

	public string PlayerId { get; set; }
	public int GameSessionId { get; set; }
	public List<Card> CardsInHand { get; set; }
}
