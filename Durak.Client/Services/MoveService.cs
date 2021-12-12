using System.Linq;
using Durak.Core;
using Durak.Core.Events;
using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Fields;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;

namespace Durak.Client.Services;

	public class MoveService : IMoveService
	{
		public MoveService(IRepository<GameCard> gameCardRepository,IFieldValidator fieldValidator,IRepository<Field> field)
		{
			_fieldValidator = fieldValidator;
			_fieldRepository = field;
		}

		private readonly IRepository<Field> _fieldRepository;
		private readonly IFieldValidator _fieldValidator;


		public void PlaceCard(Game game, Card card, Player player)
		{
			if (game.GameState != GameState.Ongoing)// || !player.PlayerHand.Cards.Contains(gameCard))
				throw new InvalidOperationException("Cant place card in this game");

			if (_fieldValidator.CanPlaceAnotherCard(game.Field,player,card))
			{
				var drawnCard = player.PlayerHand.DrawCard(card);

				game.Field.PlayCard(drawnCard);
			}

			_fieldRepository.Update(game.Field);
		}

		public void TakeCards(Player player,Game game)
		{
			if (!game.ValidateUserCanMove(player.AppIdentity, Role.Defender))
				throw new InvalidOperationException("Its not your turn");

			var playerToTake = game.DefencePlayer;

			foreach (var playedCard in game.Field.PlayedCards)
			{
				game.Field.RemoveCard(playedCard);
				playerToTake.PlayerHand.AddCard(playedCard);
			}
			PassTurn(game,player);
		}

		public void DefendFromCard(Game game, Card playerCard, Card enemyCard)
		{
			if (!game.ValidateUserCanMove(game.CurrentPlayer.AppIdentity, Role.Defender))
				throw new InvalidOperationException("Please wait your turn!");

			var isYourCardIsTrump = playerCard.Lear == game.TrumpLear;
			var isEnemyCardIsTrump = enemyCard.Lear == game.TrumpLear;

			if (playerCard.TryBeatAnother(enemyCard, isYourCardIsTrump, isEnemyCardIsTrump))
			{
				var gameCard = game.CurrentPlayer.PlayerHand.DrawCard(playerCard);
				var enemyGameCard = game.Field.PlayedCards.First(card => card.Card == enemyCard);
				game.Field.PlayCardToDefend(gameCard, enemyGameCard);
			}
		}

		public void PassTurn(Game game, Player player)
		{
			game.ValidateUserCanMove(player.AppIdentity, Role.Both);

			game.PassMove();
		}

		public void HandsUp(Game game, Player player)
		{
			game.ValidateUserCanMove(player.AppIdentity, Role.Attacker);

			game.HandsUp();
		}
	}
