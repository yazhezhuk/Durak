using Durak.Core;
using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Durak.Infrastructure.Integration;

[Authorize]
public class GameHubService : Hub
{
	private readonly IHubContext<GameHubService> _context;
	private readonly ILogger _logger;
	private static readonly Dictionary<string, string> _connections =
		new Dictionary<string, string>();

	public GameHubService(IHubContext<GameHubService> context,ILogger<GameHubService> logger)
	{
		_context = context;
		_logger = logger;
		_logger.LogInformation("Hub created");
	}

	public override Task OnConnectedAsync()
	{
		var connectionsUser = Context.UserIdentifier;
	   var connectionsIdentity = Context.ConnectionId;

	   if (connectionsUser == null || connectionsIdentity == null)
	   {
		   _logger.LogInformation("Hub connection established with errors");
		   return base.OnConnectedAsync();
	   }

	   if (_connections.ContainsKey(connectionsUser))
	   {
		   _connections[connectionsUser] = connectionsIdentity;
		   return base.OnConnectedAsync();
	   }

	   _connections.Add(connectionsUser,connectionsIdentity);

		return base.OnConnectedAsync();
	}

	public Task StartGameForUser(AppUser asUser, Game game) =>
		_context.Clients.Client(_connections[asUser.UserName])
			.SendAsync("GameStarted", game.ToViewModel(asUser));

	public Task PlaceCardOnField(Card card) =>
		_context.Clients.All.SendAsync("CardAddedToField", card);

	public Task CardRemovedFromHand(AppUser fromUser,Card card) =>
		_context.Clients.Client(_connections[fromUser.UserName])
			.SendAsync("CardRemovedFromHand", card);

	public Task PassMoveTo(Player toUser) =>
		_context.Clients.Client(_connections[toUser.AppIdentity.UserName])
			.SendAsync("MovePassedTo", toUser.CurrentRole );
	public Task PassMoveFrom(Player fromUser) =>
		_context.Clients.Client(_connections[fromUser.AppIdentity.UserName])
			.SendAsync("MovePassedFrom", fromUser.CurrentRole);

	public Task PlaceCardOnAnotherCard(Card upperCard,Card lowerCard) =>
		_context.Clients.All.SendAsync("CardPlacedOnAnother", upperCard, lowerCard);

	public Task InvalidActionOccured(AppUser user,string message) =>
		_context.Clients.Client(_connections[user.UserName])
			.SendAsync("InvalidActionOccured", message);


}
