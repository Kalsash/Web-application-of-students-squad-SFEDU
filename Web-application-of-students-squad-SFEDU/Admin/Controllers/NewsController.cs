using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Web_application_of_students_squad_SFEDU.Models;

namespace Web_application_of_students_squad_SFEDU.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class NewsController : Controller
    {
        private readonly ArticlesRepository articlesRepository;
        public NewsController(ArticlesRepository articlesRepository)
        {
            this.articlesRepository = articlesRepository;
        }

        //выбираем все записи из БД и передаем их в представление
        public IActionResult Index(Guid id)
        {
            var model = articlesRepository.GetArticles();
            if (id != default)
            {
                return View("Show", articlesRepository.GetArticleById(id));
            }
            return View(model); ;

        }

        public IActionResult NewsEdit(Guid id)
        {
            //либо создаем новую статью, либо выбираем существующую и передаем в качестве модели в представление
            Article model = id == default ? new Article() : articlesRepository.GetArticleById(id);
            return View(model);
        }
        [HttpPost] //в POST-версии метода сохраняем/обновляем запись в БД
        public IActionResult NewsEdit(Article model)
        {
            if (ModelState.IsValid)
            {
                articlesRepository.SaveArticle(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost] //т.к. удаление статьи изменяет состояние приложения, нельзя использовать метод GET
        public IActionResult NewsDelete(Guid id)
        {
            articlesRepository.DeleteArticle(new Article() { Id = id });
            return RedirectToAction("Index");
        }
    }
}
