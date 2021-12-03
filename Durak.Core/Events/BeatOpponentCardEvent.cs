using Durak.Core.Game;
using MediatR;

namespace Durak.Core.Events
{
	public class BeatOpponentCardEvent : BaseEvent
	{

		private int _playerId;
		private Card _card;


		public BeatOpponentCardEvent(int playerId, Card card)
		{
			_playerId = playerId;
			_card = card;
		}
	}
}
