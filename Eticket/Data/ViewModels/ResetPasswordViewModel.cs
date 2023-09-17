using System.ComponentModel.DataAnnotations;

namespace Eticket.Data.ViewModels
{
	public class ResetPasswordViewModel
	{

		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "Confirm  Password Is Required")]
		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "Confirm Password Don't Match")]
		public string ConfirmPassword { get; set; }
	}
}
