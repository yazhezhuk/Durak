using Durak.Core.GameModels;

namespace Durak.Core.Events.EventHandlers;

public class InvalidOperationEventHandler : BaseEventHandler<InvalidOperationEvent>
{

	public InvalidOperationEventHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{ }

	public override Task Handle(InvalidOperationEvent notification, CancellationToken cancellationToken) =>
		GameHubService.InvalidActionOccured(ResolvePlayerIdentity(notification.ActionClaimantId).AppIdentity,
			notification.ErrorMessage);

}
