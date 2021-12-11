using Durak.Core.Events.IntegrationEvents;

namespace Durak.Core.Interfaces;

public interface IIntegrationEventPublisher
{
	public Task PublishEvent(BaseIntegrationEvent integrationEvent);
}
