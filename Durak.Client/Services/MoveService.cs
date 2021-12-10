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


		public void PlaceCard(Game game, Card card, Player user)
		{
			if (user.Id != game.AttackPlayer.Id)
			{
				game.Events.Add(new ErrorEvent());
				return;
			}

			if (_fieldValidator.IsFieldEmpty(game.Field) ||
			    (_fieldValidator.CanPlaceAnotherCard(game.Field,user,card)))
			{
				var playedCard = new GameCard(game.Id, card, user.Id);
				game.Field.PlayCard(playedCard);

			}

			_fieldRepository.Update(game.Field);
		}

		public void TakeCards()
		{ }

		public void BeatCard()
		{ }

		public void EndTurn()
		{ }
	}
