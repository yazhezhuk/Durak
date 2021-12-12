using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Durak.Infrastructure.Data;

public class GameSessionRepository : BaseEfRepository<GameSession>,IGameSessionRepository
{

	public GameSessionRepository(GameContext dataContext) : base(dataContext)
	{ }

	public GameSession GetByGameName(string gameName)
	{
		return _dataContext.Set<GameSession>()
			       .Include(session => session.Game)
			       .FirstOrDefault(session => session.Game.Name.Equals(gameName)) ??
		       throw new InvalidOperationException("Game does not exist");
	}

	public GameSession GetByUserName(string userName)
	{
		return _dataContext.Set<GameSession>()
			       .Include(session => session.Game)
			       .FirstOrDefault(session =>
				       session.Game.Players.Any(player =>
					       player.AppIdentity.UserName == userName))
		       ?? throw new InvalidOperationException("Game does not exist");
	}
}
