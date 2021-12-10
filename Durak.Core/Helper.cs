using System.Collections.Immutable;
using Durak.Core.GameModels.Cards;

namespace Durak.Core;

public static class Helper
{
	public static class Game
	{
		public const int HAND_SIZE = 6;
		public static IReadOnlyCollection<Card> InitialCardSet = GenerateCardSet();

		private static IReadOnlyCollection<Card> GenerateCardSet()
		{
			ICollection<Card> cards = new List<Card>();

			foreach (var lear in Enum.GetValues(typeof(Lear)).Cast<Lear>())
			{
				foreach (var rank in Enum.GetValues(typeof(Rank)).Cast<Rank>())
				{
					var card = new Card(lear, rank);
					cards.Add(card);
				}
			}
			return cards.ToImmutableList();
		}
	}

	public static class ApplicationOptions
	{
		public static string DEFAULT_HOST = "http://localhost:3000";
		public static string DEFAULT_SECRET = "somesercet322222!";
	}

}
