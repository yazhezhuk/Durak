using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using Durak.Core.Events;
using Durak.Core.Events.ApplicationEvents;
using Durak.Core.Events.IntegrationEvents;
using Durak.Core.GameModels.Shared;
using Durak.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Durak.Core.GameModels.Cards;
[Owned]
public record Card : IJsonSerializable, IRootEntity
{
	public Lear Lear { get; set; }
	public Rank Rank { get; set; }

	[JsonConstructorAttribute]
	public Card(string lear, string rank) :
		this(Enum.Parse<Lear>(lear),Enum.Parse<Rank>(rank))
	{

	}

	public Card(Lear lear, Rank rank)
	{
		Lear = lear;
		Rank = rank;
	}

	public BaseApplicationEvent BeatAnother(GameCard enemyCard, bool isEnemyCardPrime)
	{
		var isYourCardIsPrime = isEnemyCardPrime && Lear == enemyCard.Card.Lear;

		if (isEnemyCardPrime &&
		    (isEnemyCardPrime != isYourCardIsPrime || enemyCard.Card.Rank > Rank))
			return new BadCardApplicationEvent();

		return new BeatOpponentCardApplicationEvent(this,enemyCard.Card);
	}

	public string ToJson()
	{
		return JsonConvert.SerializeObject(this);
	}
}
