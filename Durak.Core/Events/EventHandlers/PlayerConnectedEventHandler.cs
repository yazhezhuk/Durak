using Durak.Core.Events.ApplicationEvents;
using Durak.Core.Events.IntegrationEvents;
using Durak.Core.GameModels.CardSets;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using MediatR;

namespace Durak.Core.Events.EventHandlers;

public class PlayerConnectedEventHandler : INotificationHandler<PlayerConnectedEvent>
{
	private readonly IRepository<PlayerHand> _playerHandRepository;
	private readonly IRepository<Player> _playerRepository;
	private readonly IRepository<Deck> _deckRepository;
	private readonly IMediator _mediator;

	public PlayerConnectedEventHandler(
		IMediator mediator,
		IRepository<PlayerHand> playerHandRep,
		IRepository<Player> playerRep,
		IRepository<Deck> deckRep)
	{
		_mediator = mediator;
		_deckRepository = deckRep;
		_playerHandRepository = playerHandRep;
		_playerRepository = playerRep;
	}

	public Task Handle(PlayerConnectedEvent notification, CancellationToken cancellationToken)
	{
		var player = new Player(
			notification.TargetedSession.Game.Id,
			notification.User.Id);
		_playerRepository.Add(player);

		var playerHand = new PlayerHand(player.Id);
		_playerHandRepository.Add(playerHand);

		if (notification.TargetedSession.FirstPlayerConnected &&
		    notification.TargetedSession.SecondPlayerConnected)
			_mediator.Publish(new StartGameEvent(playerHand));

		player.TakeEnoughCards(notification.TargetedSession.Game.Deck);

		return Task.CompletedTask;
	}
}
