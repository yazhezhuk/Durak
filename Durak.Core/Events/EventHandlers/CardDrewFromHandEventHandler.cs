using Durak.Core.Events.ApplicationEvents;
using Durak.Core.GameModels;
using Durak.Core.GameModels.Players;
using Durak.Core.Interfaces;
using Durak.Infrastructure.Integration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Durak.Core.Events.EventHandlers;

public class CardDrewFromHandEventHandler : BaseEventHandler<CardDrawnFromHandApplicationEvent>
{

	public CardDrewFromHandEventHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{ }

	public override Task Handle(CardDrawnFromHandApplicationEvent notification, CancellationToken cancellationToken)
	{
		using var scope = ServiceProvider.CreateScope();
		var scopeServiceProvider = scope.ServiceProvider;
		var playerRepository = scopeServiceProvider.GetService<IRepository<Player>>();

		var user = playerRepository.Get(notification.ActionClaimantId).AppIdentity;

		Logger.LogInformation("Card {} has been drawn from card",notification.Card);
		return GameHubService.CardRemovedFromHand(user,notification.Card);
	}
}
