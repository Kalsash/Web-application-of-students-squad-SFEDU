using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Web_application_of_students_squad_SFEDU.Models;

namespace Web_application_of_students_squad_SFEDU.Admin.Controllers
{
    [Authorize(Roles = "moderator")]
    public class JoinController : Controller
        {
            private readonly JoinRepository JoinRepository;
            public JoinController(JoinRepository JoinRepository)
            {
                this.JoinRepository = JoinRepository;
            }

            //выбираем все записи из БД и передаем их в представление
            public IActionResult Index(Guid id)
            {
                var model = JoinRepository.GetArticles();
                return View(model);
            }

            [HttpPost] //т.к. удаление статьи изменяет состояние приложения, нельзя использовать метод GET
            public IActionResult JoinDelete(Guid id)
            {
                JoinRepository.DeleteArticle(new Join { Id = id });
                return RedirectToAction("Index");
            }
        }
}
