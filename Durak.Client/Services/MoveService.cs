using Durak.Core.Events;
using Durak.Core.Game;
using Durak.Core.Interfaces;
using Microsoft.AspNetCore.Http;

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
			if (player.Id != game.AttackPlayerId)
			{
				game.Events.Add(new ErrorEvent());
				return;
			}

			if (_fieldValidator.IsFieldEmpty(game.Field) ||
			    (_fieldValidator.CanPlaceAnotherCard(game.Field,player,card)))
			{
				var playedCard = new PlacedCard(game.Id,card.Id, player.Id);
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
