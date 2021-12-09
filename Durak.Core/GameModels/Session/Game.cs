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
	public string Name { get; set; }

	public int FieldId { get; set; }

	public Game(string name, int fieldId)
	{
		Name = name;
		FieldId = fieldId;
		GameState = GameState.Created;
	}

	public virtual List<Move> Moves { get; set; }

	public int AttackPlayerId { get; set; }
	public ConnectedPlayer AttackPlayer { get; set; }

	public int DefencePlayerId { get; set; }
	public ConnectedPlayer DefencePlayer { get; set; }

	public GameState GameState;

	public Lear TrumpLear { get; set; }

	public Move CurrentMove => Moves.Last();

	public void OnGameCreation()
	{
		var deck = GenerateDefaultDeck();
	}

	public void RollTrumpLear()
	{
		if ((int)TrumpLear == 0)
			throw new InvalidOperationException("Trump lear already decided");

		//4 - lear count
		var randomNum = Random.Shared.Next(1, 4);

		TrumpLear = (Lear)randomNum;
	}

	private Field GenerateDefaultField(Deck deck)
	{
		var field = new Field();
		field.Deck = deck;

		return null;
	}




	private Deck GenerateDefaultDeck()
	{

	}
}
