using System.Linq;
using Durak.Core;
using Durak.Core.Events;
using Durak.Core.Events.EventHandlers;
using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Fields;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;
using Durak.Core.Interfaces;
using MediatR;

namespace Durak.Client.Services;

	public class MoveService : IMoveService
	{
		public MoveService(IMediator mediator,IRepository<GameCard> gameCardRepository,IFieldValidator fieldValidator,IRepository<Field> field)
		{
			_mediator = mediator;
			_fieldValidator = fieldValidator;
			_fieldRepository = field;
		}

		private readonly IRepository<Field> _fieldRepository;
		private readonly IMediator _mediator;
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
			game.PassMove();
		}

		public void DefendFromCard(Game game, Card playerCard, Card enemyCard)
		{
			var isYourCardIsTrump = playerCard.Lear == game.TrumpLear;
			var isEnemyCardIsTrump = enemyCard.Lear == game.TrumpLear;

			if (!playerCard.TryBeatAnother(enemyCard,
				    isYourCardIsTrump,
				    isEnemyCardIsTrump))
			{
				_mediator.Publish(new InvalidOperationEvent(game.CurrentPlayer,
					"Cannot defend with this card"));
				return;
			}

			var gameCard = game.CurrentPlayer.PlayerHand.DrawCard(playerCard);
			var enemyGameCard = game.Field.PlayedCards.First(card => card.Card == enemyCard);
			game.Field.PlayCardToDefend(gameCard, enemyGameCard);
		}


		public void HandsUp(Game game, Player player)
		{
			game.ValidateUserCanMove(player.AppIdentity, Role.Attacker);

			game.HandsUp();
		}
	}
