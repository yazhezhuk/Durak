namespace Durak.Core.GameModels.Moves;

public class MoveResult
{
	private int _playerId;
	public bool Result { get; }

	public MoveResult(bool result)
	{
		_playerId = 0;
		Result= result;
	}
}
