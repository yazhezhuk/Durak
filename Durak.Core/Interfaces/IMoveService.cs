using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Session;

namespace Durak.Core.Interfaces;
public interface IMoveService
{
	void PlaceCard(Game game, Card card, Player player);
	void TakeCards(Player player,Game game);
	void DefendFromCard(Game game, Card playerCard, Card enemyCard);
	void HandsUp(Game game, Player player);

}
