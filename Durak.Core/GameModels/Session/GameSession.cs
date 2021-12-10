using Durak.Core.GameModels.Players;
using Durak.Core.GameModels.Shared;
using Durak.Core.Interfaces;

namespace Durak.Core.GameModels.Session;

public class GameSession : BaseEntity<int>, IRootEntity
{
	public Game Game { get; }

	public string? FirstUserId { get; set; }
	public string? SecondUserId { get; set; }

	public bool FirstPlayerConnected => FirstUserId != "";
	public bool SecondPlayerConnected => SecondUserId != "";
}
