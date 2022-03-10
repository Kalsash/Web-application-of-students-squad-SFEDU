using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Web_application_of_students_squad_SFEDU.Models;

namespace Web_application_of_students_squad_SFEDU.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class PhotoesController : Controller
    {
        private readonly GaleryRepository galeryRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        public PhotoesController(GaleryRepository galeryRepository, IWebHostEnvironment hostingEnvironment)
        {
            this.galeryRepository = galeryRepository;
            this.hostingEnvironment = hostingEnvironment;
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

        public IActionResult PhotoEdit(Guid id)
        {
            //либо создаем новую статью, либо выбираем существующую и передаем в качестве модели в представление
            Galery model = id == default ? new Galery() : galeryRepository.GetArticleById(id);
            return View(model);
        }
        [HttpPost] //в POST-версии метода сохраняем/обновляем запись в БД
        public IActionResult PhotoEdit(Galery model, IFormFile titleImageFile)
        {
            if (ModelState.IsValid)
            {
                if (titleImageFile != null)
                {
                    model.TitleImagePath = titleImageFile.FileName;
                    using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                galeryRepository.SaveArticle(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost] //т.к. удаление статьи изменяет состояние приложения, нельзя использовать метод GET
        public IActionResult PhotoDelete(Guid id)
        {
            galeryRepository.DeleteArticle(new Galery() { Id = id });
            return RedirectToAction("Index");
        }
    }
}
