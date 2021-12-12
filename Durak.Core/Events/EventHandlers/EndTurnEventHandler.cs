using Durak.Core.Events.ApplicationEvents;
using Durak.Core.Events.IntegrationEvents;
using Durak.Core.GameModels;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Durak.Core.Events.EventHandlers;

public class EndTurnEventHandler : BaseEventHandler<EndTurnApplicationEvent>
{
	public EndTurnEventHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	public override Task Handle(EndTurnApplicationEvent notification, CancellationToken cancellationToken)
	{
		using var scope = _serviceProvider.CreateScope();
		var scopeServiceProvider = scope.ServiceProvider;
		var player = notification.Game.CurrentPlayer;
		var gameRepository = scopeServiceProvider.GetService<IRepository<Game>>();


		Logger.LogInformation(player.CurrentRole == Role.Attacker
			? $"Player {player.AppIdentity.UserName} ends turn and hands up;"
			: $"Player {player.AppIdentity.UserName} ends turn and take cards;");
		if (notification.IsPlayerDefended)
			notification.Game.ChangeSides();
		notification.Game.PassMove();

		gameRepository.Update(notification.Game);

		return EventPublisher.PublishEvent(new TurnEndIntegrationEvent(notification.Game));
	}
}
