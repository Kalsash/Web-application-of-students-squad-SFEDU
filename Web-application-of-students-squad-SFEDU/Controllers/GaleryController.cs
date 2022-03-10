using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Web_application_of_students_squad_SFEDU.Models;

namespace Web_application_of_students_squad_SFEDU.Controllers
{
    public class GaleryController : Controller
    {
        private readonly GaleryRepository galeryRepository;
        public GaleryController(GaleryRepository galeryRepository)
        {
            this.galeryRepository = galeryRepository;
        }

        //выбираем все записи из БД и передаем их в представление
        public IActionResult Index(Guid id)
        {
            var model = galeryRepository.GetArticles();
            if (id != default)
            {
                return View("Show", galeryRepository.GetArticleById(id));
            }
            return View(model);
        }


    }
}
