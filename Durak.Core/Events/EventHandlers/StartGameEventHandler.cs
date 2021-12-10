using Durak.Core.Events.IntegrationEvents;
using Durak.Core.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Durak.Core.Events.EventHandlers;

public class StartGameEventHandler : INotificationHandler<StartGameEvent>
{
	private readonly GameHub _gameHub;

	public StartGameEventHandler(GameHub gameHub)
	{
		_gameHub = gameHub;
	}


	public Task Handle(StartGameEvent notification, CancellationToken cancellationToken)
	{
		return _gameHub.Clients.Others.SendAsync("StartGame", notification.Hand.ToJson());
	}
}
