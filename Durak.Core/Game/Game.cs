using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Durak.Core.Game.Moves;
using Durak.Core.Interfaces;

namespace Durak.Core.Game
{
	public class Game : BaseEntity<int>, IRootEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public List<Move> Moves { get; set; }

		public Field Field;
		public string AttackPlayerId { get; set; }
		public Player AttackPlayer { get; }
		public string DefencePlayerId { get; set; }
		public Player DefencePlayer { get; }

		public Lear TrumpLear { get; set; }

		public Move CurrentMove => Moves.Last();
	}
}
