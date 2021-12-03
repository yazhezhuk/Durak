namespace Durak.Core.Game
{
	public abstract record Card
	{
		public int PlayerId;
		public Lear Lear { get; }
		public Rank Rank { get; }

		protected Card(Lear lear,Rank rank)
		{
			Lear = lear;
			Rank = rank;
		}

		public abstract MoveResult BeatCard(Card enemyCard);

	}
}
