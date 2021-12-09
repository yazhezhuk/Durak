
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;


namespace Durak.Core.Interfaces;

public interface IGameSessionService
{
	Game CreateEmptyGame();
	void PlayerRequestConnection(Game game, Player player);
	void StartGame(GameSession gameSession);
}
