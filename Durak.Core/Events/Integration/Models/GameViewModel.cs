using Durak.Core.GameModels.Cards;

namespace Durak.Core.Events.Integration.Models;

public class GameViewModel
{
	public int GameId { get; set; }
	public Lear TrumpLear { get; set; }

	public List<Card> PlayerCards { get; set; }
	public int EnemyCardCount { get; set; }
}
