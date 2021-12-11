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
		public MoveService(IFieldValidator fieldValidator,IRepository<Field> field)
		{
			_fieldValidator = fieldValidator;
			_fieldRepository = field;
		}

		private readonly IRepository<Field> _fieldRepository;
		private readonly IFieldValidator _fieldValidator;


		public void PlaceCard(Game game, Card card, Player player)
		{
			var gameCard = new GameCard(game.Id, card, player.Id);
			if (game.GameState != GameState.Ongoing)// || !player.PlayerHand.Cards.Contains(gameCard))
				throw new InvalidOperationException("Cant place card in this game");

			if (_fieldValidator.IsFieldEmpty(game.Field) ||
			    (_fieldValidator.CanPlaceAnotherCard(game.Field,player,card)))
			{
				game.Field.PlayCard(gameCard);
				player.PlayerHand.DrawCard(gameCard);
			}

			_fieldRepository.Update(game.Field);
		}

		public void TakeCards(Player player,Game game)
		{
			game.ValidateUserCanMove(player.AppUser, Role.Defender);

			var playerToTake = game.DefencePlayer;

			foreach (var playedCard in game.Field.PlayedCards)
			{
				game.DefencePlayer.PlayerHand.AddCard(playedCard);
				game.Field.RemoveCard(playedCard);
			}
			PassTurn(game,player);
		}

		public void DefendFromCard(Player player, Game game, Card playerCard, Card enemyCard)
		{
			game.ValidateUserCanMove(player.AppUser, Role.Defender);

			var isYourCardIsTrump = playerCard.Lear == game.TrumpLear;
			var isEnemyCardIsTrump = enemyCard.Lear == game.TrumpLear;

			if (playerCard.TryBeatAnother(enemyCard, isEnemyCardIsTrump, isEnemyCardIsTrump))
			{
				var gameCard = new GameCard(game.Id, playerCard);
				var enemyGameCard = game.Field.PlayedCards.First(card => card.Card == enemyCard);
				game.Field.PlayCardToDefend(gameCard, enemyGameCard);
			}
		}

		public void PassTurn(Game game, Player player)
		{
			game.ValidateUserCanMove(player.AppUser, Role.Both);

			game.PassMove();
		}

		public void HandsUp(Game game, Player player)
		{
			game.ValidateUserCanMove(player.AppUser, Role.Attacker);

			game.HandsUp();
		}
	}
