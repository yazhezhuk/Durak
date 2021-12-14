using Durak.Core.Events.ApplicationEvents;

namespace Durak.Core.Events.EventHandlers;

public class BeatOpponentCardEventHandler : BaseEventHandler<BeatOpponentCardApplicationEvent>
{

	public BeatOpponentCardEventHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{ }

	public override Task Handle(BeatOpponentCardApplicationEvent notification, CancellationToken cancellationToken) =>
		GameHubService.PlaceCardOnAnotherCard(notification.SourceCard,
			notification.TargetCard);
}
