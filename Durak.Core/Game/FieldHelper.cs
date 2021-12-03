using System.Collections.Generic;
using System.Linq;
using Durak.Core.Interfaces;

namespace Durak.Core.Game
{
	public class FieldHelper : IFieldValidator
	{

		public bool CanPlaceAnotherCard(Field field,Card cardToAdd,Player player)
		{
			return IsFieldEmpty(field) ||
			       !IsCardsLimitReached(field, player) &&
			       IsCardWithSameRankPlaced(field, cardToAdd);
		}

		public bool CanEndTurn(Field field,Player player)
		{
			return field.GetCardsPlayedByOpponent(player).Count == field.BeatedCards.Count;
		}

		public bool IsCardsLimitReached(Field field, Player player)
		{
			return field.GetCardsPlayedByOpponent(player).Count > 5;
		}

		public bool IsFieldEmpty(Field field)
		{
			return field.PlayedCards.Count == 0;
		}

		public bool IsPlayerClearedTable(Field field,Player player)
		{
			return field
				       .GetCardsPlayedByOpponent(player).Count == field.BeatedCards.Count;
		}

		public bool IsCardWithSameRankPlaced(Field field,Card cardToCheck)
		{
			return field.PlayedCards.Any(card =>
				card.Rank == cardToCheck.Rank);
		}
	}
}
