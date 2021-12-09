using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Fields;
using Durak.Core.GameModels.Players;
using Durak.Core.Interfaces;

namespace Durak.Core.Services;

public class FieldValidator : IFieldValidator
{

	public bool CanPlaceAnotherCard(Field field,Player player,Card cardToAdd)
	{
		return IsFieldEmpty(field) ||
		       !IsCardsLimitReached(field, player) &&
		       IsCardWithSameRankPlaced(field, cardToAdd);
	}

	private bool IsCardsLimitReached(Field field, Player player)
	{
		return field.ListCardsByPlayer(player).Count > 5;
	}

	public bool IsFieldEmpty(Field field)
	{
		return field.PlayedCards.Count == 0;
	}

	public bool IsPlayerClearedTable(Field field,Player enemyPlayer,Player currentPlayer)
	{
		return field.ListCardsByPlayer(enemyPlayer).Count ==
		       field.ListCardsByPlayer(currentPlayer).Count;
	}

	public bool IsCardWithSameRankPlaced(Field field,Card cardToCheck)
	{
		return field.PlayedCards.Any(card =>
			card.Card.Rank == cardToCheck.Rank);
	}
}
