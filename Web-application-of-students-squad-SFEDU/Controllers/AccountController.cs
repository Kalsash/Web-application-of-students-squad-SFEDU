using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_application_of_students_squad_SFEDU.ViewModels;
using Web_application_of_students_squad_SFEDU.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Web_application_of_students_squad_SFEDU.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //public IActionResult Profile() => View(_userManager.Users.ToList());


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };

                var user2 = await _userManager.FindByNameAsync(model.Email);
                if (user2 != null)
                    return Content("Пользователь с данным Email уже зарегестрирован!");
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user); //токен для подтверждения почты
                    var callbackUrl = Url.Action("ConfirmEmail", "Account",new { userId = user.Id, code = code },protocol: HttpContext.Request.Scheme);
                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email,
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                    //return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");

                    return RedirectToAction("Profile", "Home");

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View("Error");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    // проверяем, подтвержден ли email
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "Вы не подтвердили свой email через почту");
                        return View(model);
                    }
                }

                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

