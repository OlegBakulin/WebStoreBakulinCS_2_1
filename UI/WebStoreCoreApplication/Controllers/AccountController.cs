using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreCoreApplication.Domain.ViewModels;
using WebStoreCoreApplication.Domain.Entities.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace WebStoreCoreApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }
            using (_logger.BeginScope("Вход {0} в систему", model.UserName))
            {
                var loginResult = 
                    await _signInManager.PasswordSignInAsync
                    (model.UserName, model.Password, model.RememberMe, false);

                if (!loginResult.Succeeded)
                {
                    _logger.LogWarning("Ошибка пароля");
                    ModelState.AddModelError("", "Вход невозможен");
                    return View(model);
                }

                _logger.LogInformation("{0} enter system", model.UserName);
                if (Url.IsLocalUrl(model.ReturnUrl))
                {
                    _logger.LogInformation("User {0} go to {1}", model.UserName, model.ReturnUrl);
                    return Redirect(model.ReturnUrl);
                }
            }_logger.LogInformation("User {0} go to HOME PAGE", model.UserName);
            return RedirectToAction("Index", "Base");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }
            using (_logger.BeginScope("Регистрация пользователя {0}", model.UserName))
            {
                _logger.LogInformation("Регистрация пользователя {0}", model.UserName);

                var user = new User { UserName = model.UserName, Email = model.Email };

                var createResult = await _userManager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    _logger.LogInformation("Регистрация {0} успешна", model.UserName);

                    await _userManager.AddToRoleAsync(user, "User");
                    _logger.LogInformation("{0} роль {1}", model.UserName, Role.User);

                    await _signInManager.SignInAsync(user, false);
                    _logger.LogInformation("{0} вошёл в систему", model.UserName);

                    return RedirectToAction("Index", "Base");

                }

                foreach (var identityError in createResult.Errors)//выводим ошибки
                {
                    ModelState.AddModelError("", identityError.Description);

                }
                _logger.LogWarning("Ошибка Регистрации {0} : {1}", model.UserName, string.Join(" , ", createResult.Errors.Select(er => er.Description)));
            }
            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var username = User.Identity!.Name;
            
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User {0} LogOUT", username);
            return RedirectToAction("Index", "Base");
        }
    }
}
