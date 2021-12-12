using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using MediatR;

namespace Durak.Core.Events.ApplicationEvents;

public class PlayerConnectedApplicationEvent : BaseApplicationEvent
{
	public AppUser AppUser;
	public GameSession TargetedSession;

	public PlayerConnectedApplicationEvent(GameSession connectionTarget,Player appUser)
		: base(appUser)
	{
		TargetedSession = connectionTarget;
		AppUser = appUser.AppIdentity;
	}
	public PlayerConnectedApplicationEvent(GameSession connectionTarget,AppUser appUser)
		: base(new Player(0,""))
	{
		TargetedSession = connectionTarget;
		AppUser = appUser;
	}
}
