using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace Durak.Client.Controllers;

public class UserIdProvider : IUserIdProvider
	{
		public virtual string GetUserId(HubConnectionContext connection)
		{
			return connection.User?.FindFirst(ClaimTypes.Name)?.Value;
		}
	}
