namespace Durak.Core.Game
{
	public record CommonCard : Card
	{

		public CommonCard(Lear lear, Rank rank) : base(lear, rank)
		{ }

		public override MoveResult BeatCard(Card enemyCard)
		{
			bool moveResult;
			if (enemyCard.Lear == Lear)
				moveResult = enemyCard.Rank < Rank;
			else
				moveResult = false;

			return new MoveResult(moveResult);
		}
	}
}
