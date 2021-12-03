using System.Collections.Generic;

namespace Durak.Core.Game
{
	public class Player : BaseEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public List<Card> CardsInHands { get; set; }
	}
}
