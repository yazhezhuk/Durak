namespace Durak.Core.Game
{
	public abstract class MoveContext
	{
		protected MoveContext(Fields.Field field, Card sourceCard, Card targetCard)
		{
			Field = field;
			SourceCard = sourceCard;
		}
		public Fields.Field Field { get; init; }
		public Card SourceCard { get; init; }
		public Card? TargetCard { get; init; }
	}
}
