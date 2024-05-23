using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using son_gun.Models;
using son_gun.ViewModels;

namespace son_gun.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User>userManager,SignInManager<User>signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            User user = new User()
            {
                UserName = registerVM.Email,
                Name = registerVM.FirstName,
                Surname = registerVM.LastName,
                Email = registerVM.Email,
            };
            var result= await _userManager.CreateAsync(user,registerVM.Password);

            if (!result.Succeeded) {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Login(LoginVM login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            User user;
            if(login.Email.Contains("@"))
            {
                user=await _userManager.FindByEmailAsync(login.Email);
            }
            else
            {
                user = await _userManager.FindByNameAsync(login.Email);

            }
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Username Or Password!");
                return View();
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Username Or Password!");
                return View();
            }
            if(result.IsLockedOut)
            {
                ModelState.AddModelError("", "Try Again Later");
                return View();
            }
            await _signInManager.SignInAsync(user, login.RememberMe);
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
