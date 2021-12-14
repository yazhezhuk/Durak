using Durak.Core.Events.ApplicationEvents;
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
		using var scope = ServiceProvider.CreateScope();

		var scopeServiceProvider = scope.ServiceProvider;
		var playerHandRepository = scopeServiceProvider.GetService<IRepository<PlayerHand>>();
		var mediator = scopeServiceProvider.GetService<IMediator>();

		var player = new Player(
			notification.TargetedSession.Game.Id,
			notification.AppUser.Id)
		{
			CurrentRole = notification.TargetedSession.Game.AttackPlayer == null
				? Role.Attacker
				: Role.Defender
		};

		PlayerRepository.Add(player);

		Logger.LogInformation(
			$"Player with user id:{notification.AppUser.Id} connected to the game session" +
			$"with id {notification.TargetedSession.Game.Id} with {player.CurrentRole.ToString()} role");

		var playerHand = new PlayerHand(player.Id);
		playerHandRepository.Add(playerHand);

		PlayerRepository.Update(player);

		player.TakeEnoughCards(notification.TargetedSession.Game.Deck);
		PlayerRepository.Update(player);

		if (notification.TargetedSession.FirstPlayerConnected &&
		    notification.TargetedSession.SecondPlayerConnected)
			mediator.Publish(new StartGameApplicationEvent(notification.TargetedSession.Game), cancellationToken);

		return Task.CompletedTask;
	}
}
