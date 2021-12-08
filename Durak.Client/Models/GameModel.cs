using System.ComponentModel.DataAnnotations;

namespace Durak.Client.Models
{
	public class GameModel
	{
		[Required(ErrorMessage = "First name is required")]
		public string Name;
	}
}
