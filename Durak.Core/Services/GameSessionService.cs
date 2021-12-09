using Durak.Core.GameModels.CardSets;
using Durak.Core.GameModels.Fields;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Durak.Core.Services;

public class GameSessionService : IGameSessionService
{
	private readonly IRepository<GameSession> _gameSessionsRepository;
	private readonly IRepository<Deck> _deckRepository;
	private readonly IRepository<Field> _fieldRepository;
	private readonly IRepository<Game> _gameRepository;
	private readonly UserManager<Player> _userManager;

	public GameSessionService(
		IRepository<GameSession> gameSessionsRepository,
		IRepository<Field> fieldRepository,
		IRepository<Game> gameRepository,
		UserManager<Player> userManager,
		IRepository<Deck> deckRepository
	)
	{
		_userManager = userManager;
		_gameSessionsRepository = gameSessionsRepository;
		_gameRepository = gameRepository;
		_deckRepository = deckRepository;
		_fieldRepository = fieldRepository;
	}

	public Game CreateEmptyGame(string name)
	{
		var deck = new Deck();
		deck.Fill();
		_deckRepository.Add(deck);

		var field = new Field(deck.Id);
		_fieldRepository.Add(field);

		var game = new Game(name,field.Id);
		_gameRepository.Add(game);
		game.FieldId = field.Id;
		_gameRepository.Update(game);

		return game;
	}



	public void PlayerRequestConnection(Player player, int gameId)
	{
		var possiblyExistingGame = _gameRepository.Get(gameId);
		if (possiblyExistingGame == null ||
		    possiblyExistingGame.GameState == GameState.Ended)
			throw new InvalidOperationException("Cannot connect to game due to error.");


	}

	public void StartGame(GameSession gameSession)
	{ }
}
