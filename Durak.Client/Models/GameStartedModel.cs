using Durak.Core.GameModels.Players;

namespace Durak.Client.Models;

public class GameStartedModel
{
	public int GameId { get; set; }
	public List<Player> Players { get; set; }
	public string TrumpLear { get; set; }
}
