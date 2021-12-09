using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Shared;
using Durak.Core.Interfaces;

namespace Durak.Core.GameModels.Session;

public class GameSession : BaseEntity<int>, IRootEntity
{
	public int GameId { get; set; }
	public string? FirstPlayerId { get; set; }
	public string? SecondPlayerId { get; set; }

	public void ConnectToSession(Player player)
	{
		if (FirstPlayerId == "")
			FirstPlayerId = player.Id;
		else
			SecondPlayerId = player.Id;
	}
}
