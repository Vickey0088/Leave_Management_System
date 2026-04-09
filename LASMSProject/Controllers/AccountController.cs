using LASMSProject.Models;
using LASMSProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LASMSProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        //        if (result.Succeeded)
        //        {   // Redirect based on Role
        //            var user = await _userManager.FindByEmailAsync(model.Email);
        //            if (await _userManager.IsInRoleAsync(user, "Admin"))
        //            {
        //                return RedirectToAction("Index", "Admin");
        //            }
        //            else if(await _userManager.IsInRoleAsync(user, "Manager"))
        //            {
        //                return RedirectToAction("Index", "Manager");
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index", "Employee");
        //            }
        //        }
        //        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //    }
        //    return View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);

                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return Json(new { success = true, role = "Admin" });
                    }
                    else if (await _userManager.IsInRoleAsync(user, "Manager"))
                    {
                        return Json(new { success = true, role = "Manager" });
                    }
                    else
                    {
                        return Json(new { success = true, role = "Employee" });
                    }
                }
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
