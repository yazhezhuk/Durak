using Durak.Core.GameModels.Players;
using Durak.Core.Interfaces;
using Durak.Infrastructure.Integration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Durak.Core.Events.EventHandlers;

public abstract class BaseEventHandler<T> : INotificationHandler<T> where T : INotification
{
	protected readonly GameHubService GameHubService;
	protected readonly IRepository<Player> PlayerRepository;

	protected readonly ILogger? Logger;
	protected readonly IServiceProvider ServiceProvider;

	protected BaseEventHandler(IServiceProvider serviceProvider)
	{
		ServiceProvider = serviceProvider;

		using var scope = serviceProvider.CreateScope();

		var scopeServiceProvider = scope.ServiceProvider;
		PlayerRepository = scopeServiceProvider.GetService<IRepository<Player>>() ?? throw new ApplicationException("Required service was unavailable");
		GameHubService = scopeServiceProvider.GetService<GameHubService>() ?? throw new ApplicationException("Required service was unavailable");
		Logger = scopeServiceProvider.GetService<ILogger<BaseEventHandler<T>>>();
	}

	protected Player ResolvePlayerIdentity(int userId) => PlayerRepository.Get(userId);

	public abstract Task Handle(T notification, CancellationToken cancellationToken);
}
