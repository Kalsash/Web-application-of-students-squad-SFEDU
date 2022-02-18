﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_application_of_students_squad_SFEDU.Models
{
    public class CustomUserValidator : IUserValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (user.Email.ToLower().EndsWith("@spam.com"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Данный домен находится в спам-базе. Выберите другой почтовый сервис"
                });
            }
            if (!user.Email.ToLower().EndsWith("@sfedu.ru"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Для регистрации можно использовать только почту sfedu.ru"
                });
            }

            //if (!user.Email.Contains("@"))
            //{
            // errors.Add(new IdentityError
            // {
            // Description = "Введен неверный адрес электронной почты"
            // });
            //}

            if (user.UserName.Contains("admin"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Ник пользователя не должен содержать слово 'admin'"
                });
            }

            //if (user.Email == "")
            //{
            // errors.Add(new IdentityError
            // {
            // Description = "Введен пустой Email"
            // });
            //}

            if (!(user.Year > 1890))
            {
                errors.Add(new IdentityError
                {
                    Description = "Год рождения должен быть больше 1890"
                });
            }
            return Task.FromResult(errors.Count == 0 ?
            IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
            //return Task.FromResult(IdentityResult.Success);
        }
    }
}