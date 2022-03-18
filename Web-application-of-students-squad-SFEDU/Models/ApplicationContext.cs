using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_application_of_students_squad_SFEDU.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Article> Articles { get; set; }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Galery> Photoes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>().HasData(new Article
            {
                Id = new Guid("716C2E99-6F6C-4472-81A5-43C56E11637C"),
                Title = "Тестовая новость",
                Text = "Добавьте какую-нибудь новость в панели администратора!",
                TitleImagePath = "emblem.jpg",
                Data = DateTime.Now
        });

            modelBuilder.Entity<Contact>().HasData(new Contact
            {
                Id = new Guid("716C2E99-6F6C-4472-81A5-43C56E11637D"),
                Email = "MyEmail@yandex.ru",
                FullName = "Victor Barinov",
                PhoneNumber = "+7555333222",
                TextMessage = "Hello World!"
            });

            modelBuilder.Entity<Galery>().HasData(new Galery
            {
                Id = new Guid("716C2E99-6F6C-4472-81A5-43C56E11637E"),
                Title = "Тестовая фотка",
                TitleImagePath = "emblem.jpg",
                Data = DateTime.Now
            });
        }
    }
}
