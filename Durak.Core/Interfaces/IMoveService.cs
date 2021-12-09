using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Players;

namespace Durak.Core.Interfaces;
public interface IMoveService
{
	void PlaceCard(GameModels.Session.Game game, Card card, Player player);
	void TakeCards();
	void BeatCard();
	void EndTurn();
}

