using System.Collections.Generic;
using System.Linq;
using Durak.Core.Events;
using Durak.Core.Interfaces;

namespace Durak.Core.Game
{
	public class Field : BaseEntity
	{
		public int Id { get; set; }


		public Deck Deck { get; set; }
		public List<Card> PlayedCards { get; set; }
		public List<Card> BeatedCards { get; set; }

		public IFieldStateEvaluator FieldStateEvaluator { get; set; }
		public FieldState State { get; }

		public List<Card> GetCardsPlayedByOpponent(Player currentPlayer)
		{
			return PlayedCards
				.Where(card => card.PlayerId != currentPlayer.Id)
				.ToList();
		}


		public void BeatCard(Card source,Card target)
		{
			var beatResult  = source.BeatCard(target);

			if (beatResult.Result)
			{
				PlayedCards.Add(source);
				BeatedCards.Add(target);

				Events.Add(new BeatOpponentCardEvent(target.PlayerId,target));
			}

		}

		public void EndTurn(Player player)
		{

		}


		public void PlayCard(Card card)
		{
			if (!CanPlayAnotherCard(card))
				return;

			PlayedCards.Add(card);



		}

		private bool CanPlayAnotherCard(Card cardToAdd)
		{
			return !PlayedCards.Any() &&
				PlayedCards.Count <=5 ||
				PlayedCards.Any(card =>
					card.Rank == cardToAdd.Rank);
		}


	}
}
