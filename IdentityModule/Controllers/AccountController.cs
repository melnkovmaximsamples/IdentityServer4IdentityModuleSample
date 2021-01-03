using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModule.Web.ViewModels;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;

namespace IdentityModule.Web.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(
            IIdentityServerInteractionService interactionService,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _interactionService = interactionService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Route("[action]")]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                Username = "User",
                Password = "123qwe",
                ReturnUrl = returnUrl
            });
        }

        [Route("[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                ModelState.AddModelError(nameof(model.Username), "User not found");
                return View(model);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (signInResult.Succeeded)
            {
                return Redirect(model.ReturnUrl);
            }

            ModelState.AddModelError(nameof(model.Username), "Something went wrong");

            return View(model);
        }

        [Route("[action]")]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel()
            {
                Username = "Whirlwind",
                Password = "3NA2dkzD",
                ConfirmPassword = "3NA2dkzD",
                Email = "maximmelnikov@icloud.com",
                ReturnUrl = returnUrl
            });
        }

        [Route("[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new IdentityUser(model.Username);
            
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                return Redirect(model.ReturnUrl);
            }

            return View(model);
        }

        [Route("[action]")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutResult = await _interactionService.GetLogoutContextAsync(logoutId);

            return Redirect(logoutResult.PostLogoutRedirectUri);
        }
    }
}
