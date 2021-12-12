using Durak.Core.Events.ApplicationEvents;
using Durak.Core.GameModels;
using Durak.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Durak.Core.Events.EventHandlers;

public class CardDrewFromHandEventHandler : BaseEventHandler<CardDrawnFromHandApplicationEvent>
{

	public CardDrewFromHandEventHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{ }

	public override Task Handle(CardDrawnFromHandApplicationEvent notification, CancellationToken cancellationToken)
	{
		using var scope = _serviceProvider.CreateScope();
		var scopeServiceProvider = scope.ServiceProvider;
		Logger.LogInformation("Card {} has been drawn from card",notification.Card);
		return Task.CompletedTask;
	}
}
