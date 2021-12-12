using Durak.Core.GameModels.Session;

namespace Durak.Core.Events.ApplicationEvents;

public class EndTurnApplicationEvent : BaseApplicationEvent
{
	public readonly Game Game;
	public readonly bool IsPlayerDefended;

	public EndTurnApplicationEvent(Game game,bool isPlayerDefended) : base(game.CurrentPlayer)
	{
		Game = game;
		IsPlayerDefended = isPlayerDefended;
	}
}
