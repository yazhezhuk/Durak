using Durak.Core.Events;

namespace Durak.Core.Game
{
	public class PlacedCard : BaseEntity<int>
	{
		public PlacedCard(int gameId,int cardId, string playerId)
		{
			CardId = cardId;
			PlayerId = playerId;
		}

		public int CardId { get; set; }
		public string PlayerId { get; set; }
		public int GameId { get; set; }

		public Card Card { get; }
	}
}
