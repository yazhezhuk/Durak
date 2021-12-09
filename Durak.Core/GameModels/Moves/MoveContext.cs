using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Fields;

namespace Durak.Core.GameModels.Moves;

public abstract class MoveContext
{
	protected MoveContext(Field field, Card sourceCard, Card targetCard)
	{
		Field = field;
		SourceCard = sourceCard;
	}
	public Field Field { get; init; }
	public Card SourceCard { get; init; }
	public Card? TargetCard { get; init; }
}
