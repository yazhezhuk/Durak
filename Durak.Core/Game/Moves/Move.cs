namespace Durak.Core.Game.Moves
{
	public class Move : BaseEntity<int>
	{

		public int GameId { get; set; }
		public Game Game { get; }

		public string CurrentPlayerId { get; set; }
		public Player CurrentPlayer { get; set; }
	}
}
