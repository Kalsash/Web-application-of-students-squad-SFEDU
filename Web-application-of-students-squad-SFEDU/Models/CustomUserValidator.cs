using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Web_application_of_students_squad_SFEDU.Models
{
    public class CustomUserValidator : IUserValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (!user.Email.ToLower().EndsWith("@sfedu.ru"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Для регистрации можно использовать только почту sfedu.ru"
                });
            }

            if (!Regex.IsMatch(user.Surname, "^[a-zA-ZА-яа-я]+$"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Фамилия должна состоять из букв!"
                });
            }
            if (!Regex.IsMatch(user.Name, "^[a-zA-ZА-яа-я]+$"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Имя должно состоять из букв!"
                });
            }
            if (!Regex.IsMatch(user.Patronymic, "^[a-zA-ZА-яа-я]+$"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Отчество должно состоять из букв!"
                });
            }
            if (!Regex.IsMatch(user.Department, "^[a-zA-ZА-яа-я]+$"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Факультет должен состоять из букв!"
                });
            }
            //if (user.Group <1 || user.Group > 100)
            //{
            //    errors.Add(new IdentityError
            //    {
            //        Description = "Неверный номер группы!"
            //    });
            //}
            //if (user.Course < 1 || user.Course > 6)
            //{
            //    errors.Add(new IdentityError
            //    {
            //        Description = "Неверный курс!"
            //    });
            //}

            return Task.FromResult(errors.Count == 0 ?
            IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
            //return Task.FromResult(IdentityResult.Success);
        }
    }
}