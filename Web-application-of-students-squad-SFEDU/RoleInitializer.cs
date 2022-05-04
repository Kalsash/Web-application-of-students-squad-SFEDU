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
            string adminPassword = "IamTheBest1337Admin!";
            string moderatorEmail = "Moderator@sfedu.ru";
            string moderatorPassword = "ProEditor4Ev3r";
            string surname = "Баринов";
            string name = "Виктор";
            string patronymic = "Петрович";
            string directionOfSquad = "Строительное";
            string nameOfSquad = "Студенческий информационный отряд <Вспышка>";
            string department = "Мехмат"; // Факультет
            string birthDate = "01.01.1999"; // Дата рождения
            string course = "Бакалавриат, 3 курс";// Курс
            string group = "ФИиИт, 10 группа"; // Группа
            string money = "Бюджет";// Бюджет или коммерция
            string vk = "https://vk.com/dmitry.kuplinov"; // Ссылка вк
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("moderator") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("moderator"));
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
                    NameOfSquad = nameOfSquad, 
                    Department = department,
                    BirthDate = birthDate,
                    Course = course,
                    Group = group,
                    Money = money,
                    VK = vk
                };
                User moderator = new User
                {
                    Email = moderatorEmail,
                    UserName = moderatorEmail,
                    EmailConfirmed = true,
                    Surname = surname,
                    Name = name,
                    Patronymic = patronymic,
                    DirectionOfSquad = directionOfSquad,
                    NameOfSquad = nameOfSquad,
                    Department = department,
                    BirthDate = birthDate,
                    Course = course,
                    Group = group,
                    Money = money,
                    VK = vk
                };
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                    await userManager.AddToRoleAsync(admin, "moderator");
                }
                result = await userManager.CreateAsync(moderator, moderatorPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(moderator, "moderator");
                }
            }
        }
    }
}