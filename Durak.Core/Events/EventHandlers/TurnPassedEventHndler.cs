using Durak.Core.Events.ApplicationEvents;
using Durak.Core.Events.IntegrationEvents;
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
		using var scope = _serviceProvider.CreateScope();
		var scopeServiceProvider = scope.ServiceProvider;
		var _playerRepository = scopeServiceProvider.GetService<IRepository<Player>>();

		Logger.LogInformation("User {} successfully passed turn",
			_playerRepository.Get((int)notification.ActionClaimantId).AppIdentity.UserName);
		return EventPublisher.PublishEvent(new TurnPassedIntegrationEvent());
	}


}
