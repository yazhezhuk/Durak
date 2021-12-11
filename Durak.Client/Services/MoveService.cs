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
			if (game.GameState != GameState.Ongoing)
				throw new InvalidOperationException("Cant place card in this game");

			if (player.Id != game.AttackPlayer.Id)
			{
				game.Events.Add(new ErrorApplicationEvent());
				return;
			}

			if (_fieldValidator.IsFieldEmpty(game.Field) ||
			    (_fieldValidator.CanPlaceAnotherCard(game.Field,player,card)))
			{
				var playedCard = new GameCard(game.Id, card, player.Id);
				game.Field.PlayCard(playedCard);

			}

			_fieldRepository.Update(game.Field);
		}

		public void TakeCards()
		{ }

		public void DefendFromCard(Game game, Card playerCard, Card enemyCard, Player player)
		{

		}

		public void EndTurn()
		{ }
	}
