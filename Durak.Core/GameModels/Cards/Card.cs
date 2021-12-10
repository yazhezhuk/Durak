using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using Durak.Core.Events;
using Durak.Core.Events.IntegrationEvents;
using Durak.Core.GameModels.Shared;
using Durak.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Durak.Core.GameModels.Cards;
[Owned]
public record Card(Lear Lear,Rank Rank) : IJsonSerializable, IRootEntity
{
	public BaseEvent BeatAnother(GameCard enemyCard, bool isEnemyCardPrime)
	{
		var isYourCardIsPrime = isEnemyCardPrime && Lear == enemyCard.Card.Lear;

		if (isEnemyCardPrime &&
		    (isEnemyCardPrime != isYourCardIsPrime || enemyCard.Card.Rank > Rank))
			return new BadCardEvent();

		return new BeatOpponentCardEvent(this,enemyCard.Card);
	}

	public string ToJson()
	{
		return JsonConvert.SerializeObject(this);
	}
}
