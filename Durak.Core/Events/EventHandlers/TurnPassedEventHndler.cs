using Durak.Core.Events.ApplicationEvents;
using Durak.Core.GameModels;
using Durak.Core.GameModels.Players;
using Durak.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Durak.Core.Events.EventHandlers;

public class TurnPassedEventHandler : BaseEventHandler<TurnPassedEvent>
{

	public TurnPassedEventHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{ }


	public override Task Handle(TurnPassedEvent notification, CancellationToken cancellationToken)
	{
		var player = ResolveUserIdentity(notification.ActionClaimantId);

		GameHubService.PassMoveFrom(player);
		GameHubService.PassMoveTo(notification.TurnReceiver.AppIdentity);

		Logger.LogInformation("User {} successfully passed turn");

		return Task.CompletedTask;
	}


}
