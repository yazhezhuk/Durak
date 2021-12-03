namespace Durak.Core.Game
{
	public record TrumpCard: Card
	{

		public TrumpCard(Lear lear, Rank rank) : base(lear, rank)
		{ }

		public override MoveResult BeatCard(Card enemyCard)
		{
			bool moveResult = true;
			if (enemyCard.Lear == Lear)
				moveResult = enemyCard.Rank < Rank;
			return new MoveResult(moveResult);
		}
	}
}
