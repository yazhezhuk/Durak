using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using MediatR;

namespace Durak.Core.Events.ApplicationEvents;

public class PlayerConnectedApplicationEvent : INotification
{
	public AppUser AppUser;
	public GameSession TargetedSession;

	public PlayerConnectedApplicationEvent(GameSession connectionTarget,AppUser appUser)
	{
		TargetedSession = connectionTarget;
		AppUser = appUser;
	}
}
