using Durak.Core.Events.ApplicationEvents;
using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.CardSets;
using Durak.Core.GameModels.Fields;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Durak.Core.Services;

public class GameSessionService : IGameSessionService
{
	private readonly IGameSessionRepository _gameSessionsRepository;
	private readonly IRepository<Deck> _deckRepository;
	private readonly IRepository<Field> _fieldRepository;
	private readonly IRepository<Game> _gameRepository;
	private readonly IRepository<GameCard> _gameCardRepository;
	private readonly IMediator _mediator;

	public GameSessionService(
		IMediator mediator,
		IRepository<GameCard> gameCardRepository,
		IGameSessionRepository gameSessionsRepository,
		IRepository<Field> fieldRepository,
		IRepository<Game> gameRepository,
		IRepository<Deck> deckRepository
	)
	{
		_mediator = mediator;
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


	public bool PlayerRequestConnection(AppUser appUser, GameSession gameSession)
	{
		if (gameSession == null)
			throw new InvalidOperationException("Game does not exist.");

		var existingGame = _gameRepository.Get(gameSession.Game.Id);
		if (existingGame == null || existingGame.GameState == GameState.Ended)
			throw new InvalidOperationException("Something went wrong.Game session exists but game info is not");

		switch (gameSession.FirstPlayerConnected)
		{
			case true when gameSession.SecondPlayerConnected:
				throw new InvalidOperationException("This session already full");
			case false:
				gameSession.FirstUserId = appUser.Id;
				_mediator.Publish(new PlayerConnectedEvent(gameSession, appUser));
				return true;
			case true:
				gameSession.SecondUserId = appUser.Id;
				_mediator.Publish(new PlayerConnectedEvent(gameSession, appUser));
				return true;
		}
	}
}
