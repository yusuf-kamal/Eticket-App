using Eticket.Data.ViewModels;
using Eticket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eticket.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController( RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
          _roleManager = roleManager;
          _userManager = userManager;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create( IdentityRole role)
        {
            if(ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                return RedirectToAction(nameof(Index));
                foreach (var err in result.Errors)
                    ModelState.AddModelError(string.Empty, err.Description);
                return View(role);
            }
            return View(role);

        }

        
        public async Task<IActionResult> Details(string id)
        {
            if (id is null)
                return NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();
            return View(role);
        }


        public async Task<IActionResult> Edit(string id)
        {
            if (id is null)
                return NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id,IdentityRole identityrole)
        {
            if (id != identityrole.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = identityrole.Name;
                    role.NormalizedName = identityrole.Name.ToUpper();

                    var result=await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index)); 
                       foreach (var err in result.Errors)
                        ModelState.AddModelError(string.Empty, err.Description);
                 

                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            return View(identityrole);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id is null)
               return NotFound();
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                         foreach (var err in result.Errors)
                    ModelState.AddModelError(string.Empty, err.Description);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddOrRemoveUsers( string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role is null)
                return NotFound();

            ViewBag.RoleId = RoleId;
            var users =new List<UserInRoleViewModel>();
            foreach (var user in _userManager.Users)
            {
                var userRole = new UserInRoleViewModel()
                {
                    UserId= user.Id,
                    UserName= user.UserName,
                };

                if(await _userManager.IsInRoleAsync(user, role.Name))
                 userRole.IsSelected = true;
                else
                    userRole.IsSelected = false;

                users.Add(userRole);
            }
            return View(users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleViewModel> models, string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role is null)
                return NotFound();
            if (ModelState.IsValid)
            {
                foreach (var item in models)
                {
                    var user = await _userManager.FindByIdAsync(item.UserId);
                    if(user is not null)
                    {
                        if (item.IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                            await _userManager.AddToRoleAsync(user, role.Name);
                        else if (!item.IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                                await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                }
                return RedirectToAction("Edit", new { id = RoleId });
            }
            return View (models);
        }
    }
}
