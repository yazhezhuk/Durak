using Durak.Core.Game;

namespace Core.Infrastructure.Data
{
	public class AppDbContextSeed
	{
		private readonly Context _context;
		public AppDbContextSeed(Context context)
		{
			_context = context;
		}

		public void Seed()
		{
			_context.Players.Re

			_context.SaveChanges();

			var deck = new Deck();
			deck.AddCard();
			_context.Add(new Deck())

		}
	}
}
