
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;


namespace Durak.Core.Interfaces;

public interface IGameSessionService
{
	Game CreateEmptyGame(string name);
	bool HandlePlayerConnection(AppUser appUser, GameSession gameSessionId);
}
