using Durak.Core.GameModels.Session;
using MediatR;

namespace Durak.Core.Events.ApplicationEvents;

public class StartGameApplicationEvent : INotification
{
	public Game Game;
	public StartGameApplicationEvent(Game game)
	{
		Game = game;
	}
}
