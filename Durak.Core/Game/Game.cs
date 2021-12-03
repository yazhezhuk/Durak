using System;
using System.Runtime.Serialization;
using Durak.Core.Interfaces;

namespace Durak.Core.Game
{
	public class Game : BaseEntity
	{
		public int Id { get; set; }

		public int FieldId { get; set; }
		public Field Field { get; }
		public int AttackPlayerId { get; set; }
		public Player AttackPlayer { get; }
		public int DefencePlayerId { get; set; }
		public Player DefencePlayer { get; }

		private IFieldValidator _fieldValidator = new FieldHelper();

		public Lear TrumpLear { get; set; }
		public int Turn { get; set; }

		public void EndTurn(Player player)
		{

			if (player == AttackPlayer && _fieldValidator.IsPlayerClearedTable(Field, DefencePlayer))
			{
				//send notification to defend player about turn end
			}

			if (!_fieldValidator.IsPlayerClearedTable(Field, PlayerToMove))
			{

				//send acceptation request to player;
				//await player response;
				//if response true => take cards else do nothing

			}



		}



	}
}
