using Durak.Core.Events.ApplicationEvents;
using Durak.Core.Events.IntegrationEvents;
using Durak.Core.GameModels;
using Durak.Core.GameModels.CardSets;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Durak.Core.Events.EventHandlers;

public class PlayerConnectedEventHandler : BaseEventHandler<PlayerConnectedApplicationEvent>
{

	public PlayerConnectedEventHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	public override Task Handle(PlayerConnectedApplicationEvent notification, CancellationToken cancellationToken)
	{
		using var scope = _serviceProvider.CreateScope();
		var scopeServiceProvider = scope.ServiceProvider;

		var _playerRepository = scopeServiceProvider.GetService<IRepository<Player>>();
		var _playerHandRepository = scopeServiceProvider.GetService<IRepository<PlayerHand>>();
		var _mediator = scopeServiceProvider.GetService<IMediator>();

		var player = new Player(
			notification.TargetedSession.Game.Id,
			notification.AppUser.Id)
		{
			CurrentRole = notification.TargetedSession.Game.AttackPlayer == null
				? Role.Attacker
				: Role.Defender
		};

		_playerRepository.Add(player);

		Logger.LogInformation(
			$"Player with user id:{notification.AppUser.Id} connected to the game session" +
			$"with id {notification.TargetedSession.Game.Id} with {player.CurrentRole.ToString()} role");

		var playerHand = new PlayerHand(player.Id);
		_playerHandRepository.Add(playerHand);

		_playerRepository.Update(player);

		player.TakeEnoughCards(notification.TargetedSession.Game.Deck);
		_playerRepository.Update(player);

		if (notification.TargetedSession.FirstPlayerConnected &&
		    notification.TargetedSession.SecondPlayerConnected)
			_mediator.Publish(new StartGameApplicationEvent(notification.TargetedSession.Game), cancellationToken);

		return Task.CompletedTask;
	}
}
