using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Durak.Core.GameModels.Players;

public class AppUser : IdentityUser, IRootEntity
{
	public Player AsGameIdentity(int gameId) =>
		Players.FirstOrDefault(player => player.Game.Id == gameId)!;

	public List<Player> Players { get; set; }
}
