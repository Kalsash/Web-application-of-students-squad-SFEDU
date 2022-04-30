using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Web_application_of_students_squad_SFEDU.Models
{
    public class JoinRepository
    {
        //класс-репозиторий напрямую обращается к контексту базы данных
        private readonly ApplicationContext context;
        public JoinRepository(ApplicationContext context)
        {
            this.context = context;
        }

        //выбрать все записи из таблицы Articles
        public IQueryable<Join> GetArticles()
        {
            return context.Join.OrderBy(x => x.Id);
        }

        //найти определенную запись по id
        public Join GetArticleById(Guid id)
        {
            return context.Join.Single(x => x.Id == id);
        }

        //сохранить новую либо обновить существующую запись в БД
        public Guid SaveArticle(Join entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

            return entity.Id;
        }

        //удалить существующую запись
        public void DeleteArticle(Join entity)
        {
            context.Join.Remove(entity);
            context.SaveChanges();
        }
    }
}
