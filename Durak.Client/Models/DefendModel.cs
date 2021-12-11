using System.ComponentModel.DataAnnotations;
using Durak.Core.GameModels.Cards;

namespace Durak.Client.Models;

public class DefendModel
{
	[Required(ErrorMessage = "Card is required")]
	public Card PlayerCard { get; set; }
	[Required(ErrorMessage = "Card is required")]
	public Card EnemyCard { get; set; }
	[Required(ErrorMessage = "Game session Id is required")]
	public string GameName { get; set; }
}
