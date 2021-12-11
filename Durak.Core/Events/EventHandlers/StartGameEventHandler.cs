using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using Durak.Core.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Durak.Core.Events.EventHandlers;

public class StartGameEventHandler : INotificationHandler<ApplicationEvents.StartGameApplicationEvent>
{
	private readonly IIntegrationEventPublisher _publisher;
	private readonly IRepository<Game> _gameRepository;

	public StartGameEventHandler(IRepository<Game> gameRepository, IIntegrationEventPublisher publisher)
	{
		_gameRepository = gameRepository;
		_publisher = publisher;
	}


	public Task Handle(ApplicationEvents.StartGameApplicationEvent notification, CancellationToken cancellationToken)
	{
		notification.Game.GameState = GameState.Ongoing;
		_gameRepository.Update(notification.Game);

		return _publisher.PublishEvent(
			new IntegrationEvents.StartGameIntegrationEvent(notification.Game));
	}
}
