using Durak.Core.Events.ApplicationEvents;
using Durak.Core.GameModels;
using Durak.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Durak.Core.Events.EventHandlers;

public class CardRemovedFromGameEventHandler : BaseEventHandler<CardRemovedFromField>
{

	public CardRemovedFromGameEventHandler(IServiceProvider serviceProvider) : base(serviceProvider)	{ }

	public override Task Handle(CardRemovedFromField notification, CancellationToken cancellationToken)
	{
		using var scope = ServiceProvider.CreateScope();
		var scopeServiceProvider = scope.ServiceProvider;
		Logger.LogInformation("Card:{} has been removed from field",notification.Card);

		return Task.CompletedTask;
	}
}
