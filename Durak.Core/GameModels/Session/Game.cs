using Durak.Core.GameModels.Cards;
using Durak.Core.GameModels.CardSets;
using Durak.Core.GameModels.Fields;
using Durak.Core.GameModels.Moves;
using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Shared;
using Durak.Core.Interfaces;

namespace Durak.Core.GameModels.Session;

public class Game : BaseEntity<int>, IRootEntity
{
	public int GameSessionId { get; set; }

	public string Name { get; set; }

	public Game(string name, int gameSessionId)
	{
		Name = name;
		GameSessionId = gameSessionId;
		GameState = GameState.Created;
		RollTrumpLear();
	}

	public Lear TrumpLear { get; set; }
	public Field Field { get; set; }
	public Deck Deck { get; set; }

	public List<Player> Players { get; set; } = new List<Player>();

	public GameState GameState { get; set; }

	public Player AttackPlayer => Players.First(
		p => p.CurrentRole == CurrentRole.Attacker);

	public Player DefencePlayer => Players.First(
		p => p.CurrentRole == CurrentRole.Defender);

	public void RollTrumpLear()
	{
		//4 - lear count
		var randomNum = Random.Shared.Next(1, 4);

		TrumpLear = (Lear)randomNum;
	}

	public void ChangeSides()
	{
		(AttackPlayer.CurrentRole, DefencePlayer.CurrentRole) = (DefencePlayer.CurrentRole, AttackPlayer.CurrentRole);
	}
}
