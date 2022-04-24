using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_application_of_students_squad_SFEDU.Models;

namespace Web_application_of_students_squad_SFEDU
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "Owner@sfedu.ru";
            string password = "IamTheBest1337Admin!";
            string surname = "Баринов";
            string name = "Виктор";
            string patronymic = "Петрович";
            string directionOfSquad = "Строительное";
            string department = "Мехмат"; // Факультет
            string birthDate = "01.01.1999"; // Дата рождения
            int course = 2;// Курс
            int group = 10; // Группа
            string money = "Бюджет";// Бюджет или коммерция
            string vk = "https://vk.com/dmitry.kuplinov"; // Ссылка вк
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("employee") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("employee"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    EmailConfirmed = true,
                    Surname = surname,
                    Name = name,
                    Patronymic = patronymic,
                    DirectionOfSquad = directionOfSquad,
                    Department = department,
                    BirthDate = birthDate,
                    Course = course,
                    Group = group,
                    Money = money,
                    VK = vk
                };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}