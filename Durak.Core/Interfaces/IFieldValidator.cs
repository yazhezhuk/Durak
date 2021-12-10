using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Fields;
using Durak.Core.GameModels.Players;

namespace Durak.Core.Interfaces;

public interface IFieldValidator
{
	bool IsFieldEmpty(Field field);
	bool IsPlayerClearedTable(Field field,Player enemyUser,Player currentUser);
	bool CanPlaceAnotherCard(Field field,Player user,Card cardToAdd);
}
