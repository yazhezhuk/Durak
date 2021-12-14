using Durak.Core.Events.ApplicationEvents;
using Durak.Core.GameModels;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using Durak.Core.Services;
using Durak.Infrastructure.Integration;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Durak.Core.Events.EventHandlers;

public class StartGameEventHandler : BaseEventHandler<StartGameApplicationEvent>
{

	public StartGameEventHandler(IServiceProvider serviceProvider) : base(serviceProvider) {  }

	public override Task Handle(StartGameApplicationEvent notification, CancellationToken cancellationToken)
	{
		using var scope = ServiceProvider.CreateScope();
		var scopeServiceProvider = scope.ServiceProvider;
		var gameRepository = scopeServiceProvider.GetService<IRepository<Game>>() ?? throw new ApplicationException();

		Logger.LogInformation("{}, Game",notification.Game.Name );

		notification.Game.AttackPlayer.CanMove = true;
		notification.Game.DefencePlayer.CanMove = false;
		notification.Game.GameState = GameState.Ongoing;

		gameRepository.Update(notification.Game);

		GameHubService.StartGameForUser(notification.Game.AttackPlayer.AppIdentity,notification.Game);
		GameHubService.StartGameForUser(notification.Game.DefencePlayer.AppIdentity, notification.Game);

		return Task.CompletedTask;
	}
}
