using Durak.Core.Events.ApplicationEvents;
using Durak.Core.GameModels;
using Durak.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Durak.Core.Events.EventHandlers;

public class CardRemovedFromDeckEventHandler : BaseEventHandler<CardRemovedFromDeckApplicationEvent>
{

	public override Task Handle(CardRemovedFromDeckApplicationEvent notification, CancellationToken cancellationToken)
	{
		using var scope = ServiceProvider.CreateScope();
		var scopeServiceProvider = scope.ServiceProvider;
		Logger.LogInformation($"Card {notification.RemovedCard} was removed from deck");
		return Task.CompletedTask;
	}

	public CardRemovedFromDeckEventHandler(IServiceProvider serviceProvider)
		: base(serviceProvider) {}
}
