using Durak.Core.Game;

namespace Durak.Core.Interfaces
{
	public interface IFieldValidator
	{
		bool IsCardWithSameRankPlaced(Field field, Card cardToCheck);
		bool IsCardsLimitReached(Field field, Player player);
		bool IsFieldEmpty(Field field);
		bool IsPlayerClearedTable(Field field, Player player);


		bool CanPlaceAnotherCard(Field field, Card cardToAdd, Player player);
		bool CanEndTurn(Field field, Player player);
	}
}
