using System.ComponentModel.DataAnnotations;

namespace Eticket.Data.ViewModels
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress]
		public string Email { get; set; }

	}
}
