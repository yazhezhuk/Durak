using Durak.Core.Game;
using Durak.Core.Game.Fields;

namespace Durak.Core.Interfaces;
public interface IMoveService
{
	void PlaceCard(Card card, Field field, Player player);
	void TakeCards();
	void BeatCard();
	void EndTurn();
}

