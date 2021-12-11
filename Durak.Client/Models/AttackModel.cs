using System.ComponentModel.DataAnnotations;
using Durak.Core.GameModels.Cards;

namespace Durak.Client.Models;

public class AttackModel
{
	[Required(ErrorMessage = "Card is required")]
	public Card Card { get; set; }
	[Required(ErrorMessage = "Game session Id is required")]
	public string GameName { get; set; }
}
