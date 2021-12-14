using Durak.Core.Events;
using Durak.Core.Events.ApplicationEvents;
using Durak.Core.Events.Integration.Models;
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

	public Player? AttackPlayer => Players.FirstOrDefault(
		p => p.CurrentRole == Role.Attacker);

	public Player? DefencePlayer => Players.FirstOrDefault(
		p => p.CurrentRole == Role.Defender);

	public Player? CurrentPlayer =>
		Players.FirstOrDefault(player => player.CanMove);

	public void RollTrumpLear()
	{
		//4 - lear count
		var randomNum = Random.Shared.Next(1, 4);

		TrumpLear = (Lear)randomNum;
	}


	public bool ValidateUserCanMove(AppUser appUser, Role _expected)
	{
		var player = Players.FirstOrDefault(user =>
			user.AppIdentity.UserName == appUser.UserName);

		return (player.CurrentRole == _expected && player.CanMove) ||
		       (_expected == Role.Both && player.CanMove);
	}

	public void PassMove()
	{
		var playerToPass = Players.FirstOrDefault(user => !user.CanMove);
		CurrentPlayer.CanMove = false;
		playerToPass.CanMove = true;

		Events.Add(new TurnPassedEvent(CurrentPlayer,playerToPass));
	}

	public void HandsUp()
	{
		Field.RemoveAllCards();
		ChangeSides();
		Events.Add(new EndTurnApplicationEvent(this,true));
	}


	public void ChangeSides()
	{
		(AttackPlayer.CurrentRole, DefencePlayer.CurrentRole) = (DefencePlayer.CurrentRole, AttackPlayer.CurrentRole);
	}

	public GameViewModel ToViewModel(AppUser ofUser) =>
		new GameViewModel()
		{
			GameId = Id,
			EnemyCardCount = Players.FirstOrDefault(player => player.AppIdentity.UserName != ofUser.UserName)
				.PlayerHand
				.Cards
				.Count,
			PlayerCards = Players.FirstOrDefault(player => player.AppIdentity.UserName == ofUser.UserName)
				.PlayerHand
				.Cards
				.Select(playedCard => playedCard.Card)
				.ToList(),
			TrumpLear = TrumpLear
		};
}
