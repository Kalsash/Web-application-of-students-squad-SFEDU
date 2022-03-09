using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
namespace Web_application_of_students_squad_SFEDU.Models
{
    public class ContactsRepository
    {
        //класс-репозиторий напрямую обращается к контексту базы данных
        private readonly ApplicationContext context;
        public ContactsRepository(ApplicationContext context)
        {
            this.context = context;
        }

        //выбрать все записи из таблицы Articles
        public IQueryable<Contact> GetArticles()
        {
            return context.Contacts.OrderBy(x => x.Id);
        }

        //найти определенную запись по id
        public Contact GetArticleById(Guid id)
        {
            return context.Contacts.Single(x => x.Id == id);
        }

        //сохранить новую либо обновить существующую запись в БД
        public Guid SaveArticle(Contact entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

            return entity.Id;
        }

        //удалить существующую запись
        public void DeleteArticle(Contact entity)
        {
            context.Contacts.Remove(entity);
            context.SaveChanges();
        }
    }
}
