using Durak.Core.Game;
using Durak.Core.Game.Fields;

namespace Durak.Core.Interfaces
{
	public interface IFieldValidator
	{
		bool IsFieldEmpty(Field field);
		bool IsPlayerClearedTable(Field field,Player enemyPlayer,Player currentPlayer);
		bool CanPlaceAnotherCard(Field field,Player player,Card cardToAdd);
	}
}
