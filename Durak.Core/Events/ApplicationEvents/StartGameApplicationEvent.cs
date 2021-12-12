using Durak.Core.GameModels.Session;
using MediatR;

namespace Durak.Core.Events.ApplicationEvents;

public class StartGameApplicationEvent : BaseApplicationEvent
{
	public Game Game;
	public StartGameApplicationEvent(Game game) : base(0)
	{
		Game = game;
	}
}
