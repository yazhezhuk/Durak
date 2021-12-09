using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Fields;
using Durak.Core.GameModels.Players;

namespace Durak.Core.Interfaces;

public interface IFieldValidator
{
	bool IsFieldEmpty(Field field);
	bool IsPlayerClearedTable(Field field,Player enemyPlayer,Player currentPlayer);
	bool CanPlaceAnotherCard(Field field,Player player,Card cardToAdd);
}
