using Durak.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Durak.Core.GameModels.Cards;
[Owned]
public record Card(Lear Lear, Rank Rank) : IJsonSerializable, IRootEntity
{
	public Lear Lear { get; set; } = Lear;
	public Rank Rank { get; set; } = Rank;

	[JsonConstructorAttribute]
	public Card(string lear, string rank) :
		this(Enum.Parse<Lear>(lear),Enum.Parse<Rank>(rank))
	{

	}

	public bool TryBeatAnother(Card enemyCard, bool isYourCardIsPrime, bool isEnemyCardPrime)
	{

		if ((isYourCardIsPrime && isEnemyCardPrime) ||
		    (!isYourCardIsPrime && !isEnemyCardPrime))
			return enemyCard.Rank < Rank;
		return !isEnemyCardPrime;
	}

	public string ToJson()
	{
		return JsonConvert.SerializeObject(this);
	}
}
