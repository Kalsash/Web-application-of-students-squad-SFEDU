using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Web_application_of_students_squad_SFEDU.Models;

namespace Web_application_of_students_squad_SFEDU.Controllers
{
    [Authorize(Roles = "admin")]
    public class ContactsController : Controller
    {
        private readonly ContactsRepository contactsRepository;
        public ContactsController(ContactsRepository contactsRepository)
        {
            this.contactsRepository = contactsRepository;
        }

        //выбираем все записи из БД и передаем их в представление
        public IActionResult Index(Guid id)
        {
            var model = contactsRepository.GetArticles();
            return View(model);
        }

        [HttpPost] //т.к. удаление статьи изменяет состояние приложения, нельзя использовать метод GET
        public IActionResult ContactDelete(Guid id)
        {
            contactsRepository.DeleteArticle(new Contact() { Id = id });
            return RedirectToAction("Index");
        }
    }
}
