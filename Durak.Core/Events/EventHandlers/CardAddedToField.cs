using Durak.Core.Events.ApplicationEvents;
using Durak.Core.GameModels;
using Durak.Core.Interfaces;
using Durak.Infrastructure.Integration;
using Microsoft.Extensions.DependencyInjection;

namespace Durak.Core.Events.EventHandlers;

public class CardAddedToFieldEventHandler : BaseEventHandler<CardAddedToFieldEvent>
{

	public CardAddedToFieldEventHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{ }

	public override Task Handle(CardAddedToFieldEvent notification, CancellationToken cancellationToken)
	{
		using var scope = ServiceProvider.CreateScope();
		var scopeServiceProvider = scope.ServiceProvider;

		return GameHubService.PlaceCardOnField(notification.Card.Card);
	}
}
