using Durak.Core.Events.ApplicationEvents;
using Durak.Core.Events.IntegrationEvents;
using Durak.Core.GameModels.CardSets;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using MediatR;

namespace Durak.Core.Events.EventHandlers;

public class PlayerConnectedEventHandler : BaseEventHandler<PlayerConnectedApplicationEvent>
{
	private readonly IRepository<PlayerHand> _playerHandRepository;
	private readonly IRepository<Player> _playerRepository;
	private readonly IRepository<Deck> _deckRepository;
	private readonly IMediator _mediator;

	public PlayerConnectedEventHandler(
		IIntegrationEventPublisher eventPublisher,
		IMediator mediator,
		IRepository<PlayerHand> playerHandRep,
		IRepository<Player> playerRep,
		IRepository<Deck> deckRep) : base(eventPublisher)
	{
		_mediator = mediator;
		_deckRepository = deckRep;
		_playerHandRepository = playerHandRep;
		_playerRepository = playerRep;
	}

	public override Task Handle(PlayerConnectedApplicationEvent notification, CancellationToken cancellationToken)
	{
		var player = new Player(
			notification.TargetedSession.Game.Id,
			notification.AppUser.Id);

		if (notification.TargetedSession.Game.AttackPlayer == null)
			player.CurrentRole = Role.Attacker;
		else
			player.CurrentRole = Role.Defender;

		_playerRepository.Add(player);

		var playerHand = new PlayerHand(player.Id);
		_playerHandRepository.Add(playerHand);
		_playerRepository.Update(player);

		player.TakeEnoughCards(notification.TargetedSession.Game.Deck);
		_playerRepository.Update(player);

		if (notification.TargetedSession.FirstPlayerConnected &&
		    notification.TargetedSession.SecondPlayerConnected)
			_mediator.Publish(new StartGameApplicationEvent(notification.TargetedSession.Game));

		return Task.CompletedTask;
	}
}
