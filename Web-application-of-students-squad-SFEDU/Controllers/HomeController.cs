using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web_application_of_students_squad_SFEDU.Models;
using Web_application_of_students_squad_SFEDU.ViewModels;

namespace Web_application_of_students_squad_SFEDU.Controllers
{
    public class HomeController : Controller
    {
        // Fields
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ArticlesRepository articlesRepository;
        private readonly ContactsRepository contactRepository;
        private readonly JoinRepository JoinRepository;


        // Constructor
        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager,
        SignInManager<User> signInManager, ArticlesRepository articlesRepository, ContactsRepository contactRepository, JoinRepository joinRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            this.articlesRepository = articlesRepository;
            this.contactRepository = contactRepository;
            JoinRepository = joinRepository;
        }

        //// Главная
        //public IActionResult Index()
        //{
        // return View();
        //}

        //выбираем все записи из БД и передаем их в представление
        public IActionResult Index(Guid id)
        {
            var model = articlesRepository.GetArticles();
            if (id != default)
            {
                return View("Show", articlesRepository.GetArticleById(id));
            }
            return View(model);
        }

        // Направления
        public IActionResult Directions()
        {
            return View();
        }
        // Структура
        public IActionResult Structure()
        {
            return View();
        }
        // О нас
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Stud()
        {
            return View();
        }

        public IActionResult Join()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Join(Join model)
        {
            if (model.SurName != null)
            {
                JoinRepository.SaveArticle(model);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contacts(Contact model)
        {
            if (model.Email != null)
            {
                contactRepository.SaveArticle(model);
                return RedirectToAction("Index");
            }
            return View();
        }


        //Профиль
        public IActionResult Profile() => View(_userManager.Users.ToList());

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        //////////// Registration ///////////////////

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        //////////// Registration ///////////////////




        //////////// Users ///////////////////
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
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

        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Surname = user.Surname,
                Name = user.Name,
                Patronymic = user.Patronymic,
                BirthDate = user.BirthDate,
                DirectionOfSquad = user.DirectionOfSquad,
                NameOfSquad = user.NameOfSquad,
                Department = user.Department,
                Course = user.Course,
                Group = user.Group,
                Money = user.Money,
                VK = user.VK
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Surname= model.Surname;
                    user.Name = model.Name;
                    user.Patronymic = model.Patronymic;
                    user.BirthDate = model.BirthDate;
                    user.DirectionOfSquad = model.DirectionOfSquad;
                    user.NameOfSquad = model.NameOfSquad;
                    user.Department = model.Department;
                    user.Course = model.Course;
                    user.Group = model.Group;
                    user.Money = model.Money;
                    user.VK = model.VK;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ChangePassword(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    IdentityResult result =
                    await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Введен неправильный старый пароль или не удовлетворяет требованиям новый пароль!");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }
        //////////// Users ///////////////////

    }
}