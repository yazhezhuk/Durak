using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using MediatR;

namespace Durak.Core.Events.ApplicationEvents;

public class PlayerConnectedEvent : INotification
{
	public User User;
	public GameSession TargetedSession;

	public PlayerConnectedEvent(GameSession connectionTarget,User user)
	{
		TargetedSession = connectionTarget;
		User = user;
	}
}
