using System.ComponentModel.DataAnnotations;

namespace Durak.Client.Models;

public class LoginModel
{
	[Required(ErrorMessage = "AppIdentity Name is required")]
	public string Username { get; set; }

	[Required(ErrorMessage = "Password is required")]
	public string Password { get; set; }
}
