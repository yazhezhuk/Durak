using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using MediatR;

namespace Durak.Core.Events.ApplicationEvents;

public class PlayerConnectedEvent : INotification
{
	public AppUser AppUser;
	public GameSession TargetedSession;

	public PlayerConnectedEvent(GameSession connectionTarget,AppUser appUser)
	{
		TargetedSession = connectionTarget;
		AppUser = appUser;
	}
}
