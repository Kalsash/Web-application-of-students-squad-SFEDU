using Microsoft.AspNetCore.Mvc;
using System;
using Web_application_of_students_squad_SFEDU.Models;

namespace Web_application_of_students_squad_SFEDU.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ArticlesRepository articlesRepository;
        public ArticlesController(ArticlesRepository articlesRepository)
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
            return View(model);
        }
    }
}
