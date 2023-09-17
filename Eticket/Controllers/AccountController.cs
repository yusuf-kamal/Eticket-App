using Eticket.Data.ViewModels;
using Eticket.Helper;
using Eticket.Models;
using ETicket.data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticket.Controllers
{
    public class AccountController : Controller
    {
        private readonly EticketDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(EticketDbContext context,UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager ,RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
			_signInManager = signInManager;
           _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> Register( RegisterViewModel viewModel)
		{
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    UserName = viewModel.Email.Split('@')[0],
                    Email = viewModel.Email,
                    IsAgree = viewModel.IsAgree,
                    IsAdmin = true,
                };

                var result = await _userManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    //await _userManager.AddToRoleAsync(user, "Admin");
                    return RedirectToAction(nameof(Login));
                }
                
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

            }
            return View(viewModel);
		}

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        public IActionResult Login()
        {
            return View();
        }

		[HttpPost]

		public async Task<IActionResult> Login(LoginViewModel loginmodel)
		{
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginmodel.Email);

               
                if (user != null)
                {
                    user.IsAdmin = true;
                    var flag = await _userManager.CheckPasswordAsync(user, loginmodel.Password);
                    if(flag)
                    {
                        

                        var result = await _signInManager.PasswordSignInAsync(user, loginmodel.Password, loginmodel.RememmberMe,false);

                        if (result.Succeeded)
                        {
                           
                                return RedirectToAction("Index", "Movies");
                        }

                    }
                    ModelState.AddModelError(string.Empty, "Password Is Not Correct");

				}
				ModelState.AddModelError(string.Empty, "Email Is Not Exited");

			}
			return View(loginmodel);
		}

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }

        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token =await _userManager.GeneratePasswordResetTokenAsync(user);
                    var PasswordResetLink = Url.Action("ResetPassword", "Account", new { email = user.Email,token },Request.Scheme);

					var email = new Email()
                    {
                        Subject = "Reset Password",
						To = user.Email,
                        Body= PasswordResetLink,

					};
                    EmailSetting.SendEmail(email);
                    return RedirectToAction(nameof(CheckYouInBox));
                }
                ModelState.AddModelError(string.Empty, "Email Is Not Existed");
            }
            return View(model);
        }


        public IActionResult CheckYouInBox()
        {
            return View();
        }


        public IActionResult ResetPassword(string email , string token)
        {
			TempData["email"] = email;
			TempData["token"] = token;

			return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
			if (ModelState.IsValid)
			{

				var email = TempData["email"] as string ;
				var token = TempData["token"] as string;
				var user = await _userManager.FindByEmailAsync(email);
				var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
				if (result.Succeeded)
					return RedirectToAction(nameof(Login));
				foreach (var err in result.Errors)

					ModelState.AddModelError(string.Empty, err.Description);
			}
			return View(model);
		}


    }
}
