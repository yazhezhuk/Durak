using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Players;

namespace Durak.Core.Events.ApplicationEvents;

public class BeatOpponentCardApplicationEvent : BaseApplicationEvent
{
	public Card SourceCard { get; set; }
	public Card TargetCard { get; set; }

	public BeatOpponentCardApplicationEvent(int player,Card surceCard, Card targerCard) : base(player)
	{
		SourceCard = surceCard;
		TargetCard = targerCard;
	}
}
