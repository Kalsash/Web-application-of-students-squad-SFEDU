using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using Web_application_of_students_squad_SFEDU.Models;

namespace Web_application_of_students_squad_SFEDU.Admin.Controllers
{
    [Authorize(Roles = "moderator")]
    public class NewsController : Controller
    {
        private readonly ArticlesRepository articlesRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        public NewsController(ArticlesRepository articlesRepository, IWebHostEnvironment hostingEnvironment)
        {
            this.articlesRepository = articlesRepository;
            this.hostingEnvironment = hostingEnvironment;
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

        public IActionResult NewsEdit(Guid id)
        {
            //либо создаем новую статью, либо выбираем существующую и передаем в качестве модели в представление
            Article model = id == default ? new Article() : articlesRepository.GetArticleById(id);
            return View(model);
        }
        [HttpPost] //в POST-версии метода сохраняем/обновляем запись в БД
        public IActionResult NewsEdit(Article model, IFormFile titleImageFile)
        {
            if (ModelState.IsValid)
            {
                if (titleImageFile != null)
                {
                    // Check if valid image type (can be extended with more rigorous checks)
                    string[] allowedImageTypes = new string[] { "image/jpeg", "image/png" };
                    if (!allowedImageTypes.Contains(titleImageFile.ContentType.ToLower())) return View(model);

                    // Prepare paths for saving images
                    string imagesPath = Path.Combine(hostingEnvironment.WebRootPath, "images/news/");
                    string webPFileName = Path.GetFileNameWithoutExtension(titleImageFile.FileName) + ".webp";
                    string normalImagePath = Path.Combine(imagesPath, titleImageFile.FileName);
                    string webPImagePath = Path.Combine(imagesPath, webPFileName);

                    // Save the image in its original format for fallback
                    using (var normalFileStream = new FileStream(normalImagePath, FileMode.Create))
                    {
                        titleImageFile.CopyTo(normalFileStream);
                    }
                    model.TitleImagePath = webPFileName;
                    // Then save in WebP format
                    using (var webPFileStream = new FileStream(webPImagePath, FileMode.Create))
                    {
                        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                        {
                            imageFactory.Load(titleImageFile.OpenReadStream())
                                        .Format(new WebPFormat())
                                        .Quality(80)
                                        .Save(webPFileStream);
                        }
                    }

                    //model.TitleImagePath = titleImageFile.FileName;

                    //using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/news/", titleImageFile.FileName), FileMode.Create))
                    //{
                    //    titleImageFile.CopyTo(stream);
                    //
                }
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
