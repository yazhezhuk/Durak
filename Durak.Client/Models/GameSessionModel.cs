using System.ComponentModel.DataAnnotations;

namespace Durak.Client.Models;

public class GameSessionModel
{
	[Required(ErrorMessage = "Game name is required")]
	public string Name { get; set; }

	public UserModel FirstUser { get; set; }
	public UserModel SecondUser { get; set; }

}
