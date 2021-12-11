using Durak.Core.GameModels.Session;

namespace Durak.Core.Events.ApplicationEvents;

public class EndTurnApplicationEvent : BaseApplicationEvent
{
	public readonly Game Game;

	public EndTurnApplicationEvent(Game game)
	{
		Game = game;
	}
}
