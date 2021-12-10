using System.ComponentModel.DataAnnotations;

namespace Durak.Client.Models;

public class GameModel
{
	[Required(ErrorMessage = "Game name is required")]
	public string Name { get; set; }
}
