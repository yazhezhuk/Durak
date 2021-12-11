using Durak.Core.Events.IntegrationEvents;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using Durak.Core.Services;
using Durak.Infrastructure.Integration;
using Microsoft.AspNetCore.SignalR;

namespace Durak.Client.Services;

public class IntegrationEventPublisher : IIntegrationEventPublisher
{
	private readonly GameHub _gameHub;

	public IntegrationEventPublisher(GameHub gameHub)
	{
		_gameHub = gameHub;
	}

	public Task PublishEvent(BaseIntegrationEvent integrationEvent)
	{
		return integrationEvent.Publish(_gameHub);
	}
}
