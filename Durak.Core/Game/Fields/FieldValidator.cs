using Durak.Core.Interfaces;

namespace Durak.Core.Game.Fields
{
	public class FieldValidator : IFieldValidator
	{

		public bool CanPlaceAnotherCard(Fields.Field field,Player player,Card cardToAdd)
		{
			return IsFieldEmpty(field) ||
			       !IsCardsLimitReached(field, player) &&
			       IsCardWithSameRankPlaced(field, cardToAdd);
		}

		private bool IsCardsLimitReached(Fields.Field field, Player player)
		{
			return field.GetCardsPlayedByPlayer(player).Count > 5;
		}

		public bool IsFieldEmpty(Fields.Field field)
		{
			return field.PlayedCards.Count == 0;
		}

		public bool IsPlayerClearedTable(Fields.Field field,Player enemyPlayer,Player currentPlayer)
		{
			return field.GetCardsPlayedByPlayer(enemyPlayer).Count ==
			       field.GetCardsPlayedByPlayer(currentPlayer).Count;
		}

		public bool IsCardWithSameRankPlaced(Fields.Field field,Card cardToCheck)
		{
			return field.PlayedCards.Any(card =>
				card.Card.Rank == cardToCheck.Rank);
		}
	}
}
