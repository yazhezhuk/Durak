using Durak.Core.GameModels.Cards;

namespace Durak.Core.Events.ApplicationEvents;

public class BeatOpponentCardApplicationEvent : BaseApplicationEvent
{
	private Card _sourceCard;
	private Card _targerCard;


	public BeatOpponentCardApplicationEvent(Card surceCard, Card targerCard)
	{
		_sourceCard = surceCard;
		_targerCard = targerCard;
	}
}
