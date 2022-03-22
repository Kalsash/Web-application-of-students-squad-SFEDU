using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Web_application_of_students_squad_SFEDU.Models
{
    public class ArticlesRepository
    {
        //класс-репозиторий напрямую обращается к контексту базы данных
        private readonly ApplicationContext context;
        public ArticlesRepository(ApplicationContext context)
        {
            this.context = context;
        }

        //выбрать все записи из таблицы Articles
        public IQueryable<Article> GetArticles()
        {
            return context.Articles.OrderByDescending(x => x.Data);
        }

        //найти определенную запись по id
        public Article GetArticleById(Guid id)
        {
            return context.Articles.Single(x => x.Id == id);
        }

        //сохранить новую либо обновить существующую запись в БД
        public Guid SaveArticle(Article entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

            return entity.Id;
        }

        //удалить существующую запись
        public void DeleteArticle(Article entity)
        {
            context.Articles.Remove(entity);
            context.SaveChanges();
        }
    }
}
