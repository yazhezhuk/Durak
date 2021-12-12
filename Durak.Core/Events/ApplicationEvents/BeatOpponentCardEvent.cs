using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Players;

namespace Durak.Core.Events.ApplicationEvents;

public class BeatOpponentCardApplicationEvent : BaseApplicationEvent
{
	private Card _sourceCard;
	private Card _targetCard;

	public BeatOpponentCardApplicationEvent(Player player,Card surceCard, Card targerCard) : base(player)
	{
		_sourceCard = surceCard;
		_targetCard = targerCard;
	}
}
