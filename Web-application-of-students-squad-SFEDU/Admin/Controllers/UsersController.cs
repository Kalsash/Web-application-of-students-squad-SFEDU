using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Web_application_of_students_squad_SFEDU.Models;
using Web_application_of_students_squad_SFEDU.ViewModels;
using Microsoft.AspNetCore.Authorization;
namespace Web_application_of_students_squad_SFEDU.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {

        UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());

        public IActionResult Create() => View();

        public IActionResult Profile() => View(_userManager.Users.ToList());

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
                    user.Surname = model.Surname;
                    user.Name = model.Name;
                    user.Patronymic = model.Patronymic;
                    user.BirthDate = model.BirthDate;
                    user.DirectionOfSquad = model.DirectionOfSquad;
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
                    var _passwordHasher =
                    HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;
                    user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Index");
                    //IdentityResult result =
                    // await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    //if (result.Succeeded)
                    //{
                    // return RedirectToAction("Index");
                    //}
                    //else
                    //{
                    // foreach (var error in result.Errors)
                    // {
                    // ModelState.AddModelError(string.Empty, error.Description);
                    // }
                    //}
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }
    }
}