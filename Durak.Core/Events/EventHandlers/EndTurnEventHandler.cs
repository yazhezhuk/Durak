using Durak.Core.Events.ApplicationEvents;
using Durak.Core.Events.IntegrationEvents;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;

namespace Durak.Core.Events.EventHandlers;

public class EndTurnEventHandler : BaseEventHandler<EndTurnApplicationEvent>
{
	private readonly IRepository<Game> _gameRepository;

	public EndTurnEventHandler(IRepository<Game> gameRepository,IIntegrationEventPublisher eventPublisher) : base(eventPublisher)
	{
		_gameRepository = gameRepository;
	}

	public override Task Handle(EndTurnApplicationEvent notification, CancellationToken cancellationToken)
	{
		if (notification.IsPlayerDefended)
			notification.Game.ChangeSides();
		notification.Game.PassMove();

		_gameRepository.Update(notification.Game);

		return _eventPublisher.PublishEvent(new TurnEndIntegrationEvent(notification.Game));
	}
}
