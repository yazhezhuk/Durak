using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Shared;

namespace Durak.Core.GameModels.Moves;

public class Move : BaseEntity<int>
{

	public int GameId { get; set; }
	public Session.Game Game { get; }

	public string CurrentPlayerId { get; set; }
	public Player CurrentPlayer { get; set; }
}
