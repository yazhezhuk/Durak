using Durak.Core.Events.ApplicationEvents;
using Durak.Core.GameModels;
using Durak.Core.GameModels.Players;
using Durak.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Durak.Core.Events.EventHandlers;

public class CardAddedToHandEventHandler : BaseEventHandler<CardAddedToHandApplicationEvent>
{

	public CardAddedToHandEventHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{

	}

	public override Task Handle(CardAddedToHandApplicationEvent notification, CancellationToken cancellationToken)

	{
		using var scope = ServiceProvider.CreateScope();
		var scopeServiceProvider = scope.ServiceProvider;
		var playerRep = scopeServiceProvider.GetService<IRepository<Player>>();

		Logger.LogInformation("Card:{} has been added to player: {}",
			notification.Card,
			playerRep.Get(notification.ActionClaimantId).AppIdentity.UserName);

			return Task.CompletedTask;
	}
}
