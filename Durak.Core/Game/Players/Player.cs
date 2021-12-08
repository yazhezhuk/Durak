using System.Collections.Generic;
using Durak.Core.Events;
using Durak.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Durak.Core.Game
{
	public class Player : IdentityUser, IRootEntity
	{
		public PlayerHand PlayerHand { get; }

		public Player(string name)
		{
			base.UserName = name;
		}
	}
}
