using System.Formats.Asn1;
using Durak.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServiceProviderServiceExtensions = Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions;

namespace Durak.Core.GameModels;

public abstract class BaseEventHandler<T> : INotificationHandler<T> where T : INotification
{
	protected readonly ILogger? Logger;
	protected readonly IServiceProvider _serviceProvider;

	protected BaseEventHandler(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
		using var scope = serviceProvider.CreateScope();
		var scopeServiceProvider = scope.ServiceProvider;
		Logger = scopeServiceProvider.GetService<ILogger<BaseEventHandler<T>>>();
	}

	public abstract Task Handle(T notification, CancellationToken cancellationToken);
}
