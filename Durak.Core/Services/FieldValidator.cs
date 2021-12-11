using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Fields;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;

namespace Durak.Core.Services;

public class FieldValidator : IFieldValidator
{

	public bool CanPlaceAnotherCard(Field field,Player user,Card cardToAdd)
	{
		return IsFieldEmpty(field) ||
		       !IsCardsLimitReached(field, user) &&
		       IsCardWithSameRankPlaced(field, cardToAdd);
	}

	private bool IsCardsLimitReached(Field field, Player user)
	{
		return field.ListPlacedCards(user).Count > 5;
	}

	public bool IsFieldEmpty(Field field)
	{
		return field.PlayedCards.Count == 0;
	}

	public bool IsPlayerClearedTable(Field field,Player enemyUser,Player currentUser)
	{
		return field.ListPlacedCards(enemyUser).Count ==
		       field.ListPlacedCards(currentUser).Count;
	}

	public bool IsCardWithSameRankPlaced(Field field,Card cardToCheck)
	{
		return field.PlayedCards.Any(card =>
			card.Card.Rank == cardToCheck.Rank);
	}

	public bool ValidateUserAttackerRight(AppUser user, Game game) =>
		game.AttackPlayer.AppUser.UserName == user.UserName;
}
