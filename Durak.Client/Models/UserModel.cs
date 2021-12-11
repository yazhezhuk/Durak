using System.ComponentModel.DataAnnotations;

namespace Durak.Client.Models;

public class UserModel
{
	[Required(ErrorMessage = "Game name is required")]
	public string Name { get; set; }
	public string Email { get; set; }
}
