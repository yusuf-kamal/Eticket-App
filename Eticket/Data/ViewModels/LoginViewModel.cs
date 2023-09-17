using System.ComponentModel.DataAnnotations;

namespace Eticket.Data.ViewModels
{
	public class LoginViewModel
	{

		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress]
		public string Email { get; set; }


		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

	

		public bool RememmberMe { get; set; }
	}
}
