using System.ComponentModel.DataAnnotations;

namespace Eticket.Data.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "Name Is Required")]

		public string FirstName { get; set; }

		[Required(ErrorMessage = "Name Is Required")]

		public string LastName { get; set; }

		[Required(ErrorMessage ="Email Is Required")]
		[EmailAddress]

		public string Email { get; set; }

		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm  Password Is Required")]
		[DataType(DataType.Password)]
		[Compare("Password",ErrorMessage ="Confirm Password Don't Match")]
		public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }
    }
}
