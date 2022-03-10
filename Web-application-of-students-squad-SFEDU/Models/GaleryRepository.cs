using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Web_application_of_students_squad_SFEDU.Models
{
    public class GaleryRepository
    {
        //класс-репозиторий напрямую обращается к контексту базы данных
        private readonly ApplicationContext context;
        public GaleryRepository(ApplicationContext context)
        {
            this.context = context;
        }

        //выбрать все записи из таблицы Articles
        public IQueryable<Galery> GetArticles()
        {
            return context.Photoes.OrderBy(x => x.Title);
        }

        //найти определенную запись по id
        public Galery GetArticleById(Guid id)
        {
            return context.Photoes.Single(x => x.Id == id);
        }

        //сохранить новую либо обновить существующую запись в БД
        public Guid SaveArticle(Galery entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

            return entity.Id;
        }

        //удалить существующую запись
        public void DeleteArticle(Galery entity)
        {
            context.Photoes.Remove(entity);
            context.SaveChanges();
        }
    }
}
