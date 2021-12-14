using Durak.Core.Events.ApplicationEvents;
using Durak.Core.GameModels;
using Durak.Core.GameModels.Players;
using Durak.Core.Interfaces;
using Durak.Infrastructure.Integration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Durak.Core.Events.EventHandlers;

public class CardTakenEventHandler : BaseEventHandler<CardTakenApplicationEvent>
{
	public CardTakenEventHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{ }

	public override Task Handle(CardTakenApplicationEvent notification, CancellationToken cancellationToken)
	{
		using var scope = ServiceProvider.CreateScope();
		var scopeServiceProvider = scope.ServiceProvider;
		var gameRepository = scopeServiceProvider.GetService<GameHubService>();


		Logger.LogInformation("Card:{} added to hand", notification.Card);
		return Task.CompletedTask;
	}


}
