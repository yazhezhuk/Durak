using Durak.Core.GameModels.Cards;
using MediatR;

namespace Durak.Core.Events;

public class BeatOpponentCardEvent : BaseEvent
{
	private Card _sourceCard;
	private Card _targerCard;


	public BeatOpponentCardEvent(Card surceCard, Card targerCard)
	{
		_sourceCard = surceCard;
		_targerCard = targerCard;
	}
}
