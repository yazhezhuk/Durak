using Durak.Core.GameModels.Session;

namespace Durak.Core.Interfaces;

public interface IGameSessionRepository : IRepository<GameSession>
{
	public GameSession GetByGameName(string gameName);
	public GameSession GetByUserName(string userName);

}
