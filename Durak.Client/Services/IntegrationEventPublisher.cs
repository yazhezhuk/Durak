using Durak.Core.Events.IntegrationEvents;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using Durak.Core.Services;
using Durak.Infrastructure.Integration;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Durak.Client.Services;

public class IntegrationEventPublisher : IIntegrationEventPublisher
{
	private readonly IServiceScopeFactory _serviceScopeFactory;

	public IntegrationEventPublisher(IServiceScopeFactory serviceScopeFactory)
	{
		_serviceScopeFactory = serviceScopeFactory;
	}

	public Task PublishEvent(BaseIntegrationEvent integrationEvent)
	{
		using(var scope = _serviceScopeFactory.CreateScope())
		{
			var gameHub = scope.ServiceProvider.GetRequiredService<GameHub>();
			var gameHubContext = scope.ServiceProvider.GetService<IHubContext<GameHub>>();


			integrationEvent.Publish(gameHubContext.Clients);
		}

		return Task.CompletedTask;
	}
}
