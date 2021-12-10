using Durak.Core.GameModels.Cards;
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
	private readonly IRepository<GameCard> _gameCardRepository;

	public GameSessionService(
		IRepository<GameCard> gameCardRepository,
		IRepository<GameSession> gameSessionsRepository,
		IRepository<Field> fieldRepository,
		IRepository<Game> gameRepository,
		IRepository<Deck> deckRepository
	)
	{
		_gameSessionsRepository = gameSessionsRepository;
		_gameCardRepository = gameCardRepository;
		_fieldRepository = fieldRepository;
		_gameRepository = gameRepository;
		_deckRepository = deckRepository;
	}

	public Game CreateEmptyGame(string name)
	{
		var gameSession = new GameSession();
		_gameSessionsRepository.Add(gameSession);

		var game = new Game(name,gameSession.Id);
		_gameRepository.Add(game);

		var deck = new Deck(game.Id);
		deck.Fill();

		_deckRepository.Add(deck);

		var field = new Field(game.Id);
		_fieldRepository.Add(field);

		_gameRepository.Update(game);

		return game;
	}



	public void PlayerRequestConnection(User user, int gameSessionId)
	{
		var existingGameSession = _gameSessionsRepository.Get(gameSessionId);
		if (existingGameSession == null)
			throw new InvalidOperationException("Game does not exist.");

		var existingGame = _gameRepository.Get(existingGameSession.Game.Id);
		if (existingGame == null || existingGame.GameState == GameState.Ended)
			throw new InvalidOperationException("Something went wrong.Game session exists but game info is not");

		switch (existingGameSession.FirstPlayerConnected)
		{
			case true when existingGameSession.SecondPlayerConnected:
				throw new InvalidOperationException("This session already full");
			case false:
				existingGameSession.FirstUserId = user.Id;
				break;
			case true:
				existingGameSession.SecondUserId = user.Id;
				break;
		}
	}
}
