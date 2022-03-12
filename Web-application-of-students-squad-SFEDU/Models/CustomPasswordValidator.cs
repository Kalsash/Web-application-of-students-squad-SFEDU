using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Web_application_of_students_squad_SFEDU.Models
{
    public class CustomPasswordValidator : IPasswordValidator<User>
    {
        public int RequiredLength { get; set; } // минимальная длина

        public CustomPasswordValidator(int length)
        {
            RequiredLength = length;
        }

        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (String.IsNullOrEmpty(password) || password.Length < RequiredLength)
            {
                errors.Add(new IdentityError
                {
                    Description = $"Минимальная длина пароля равна {RequiredLength}"
                });
            }
            string pattern = "^[0-9]+$";

            if (Regex.IsMatch(password, pattern))
            {
                errors.Add(new IdentityError
                {
                    Description = "Пароль не должен состоять только из цифр"
                });
            }

            if (Regex.IsMatch(password, @"^\D+$"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Пароль должен содержать как минимум одну цифру"
                });
            }

            string allowedSymbols = ".@abcdefghijklmnopqrstuvwxyz1234567890*#$%^&!?";

            for (int i = 0; i < password.Length - 1; i++)
            {
                if (!allowedSymbols.Contains(password.ToLower()[i]))
                {
                    errors.Add(new IdentityError
                    {
                        Description = "Пароль содержит недопустимые символы"
                    });
                    break;
                }
            }

            string LowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
            bool ContainLowerCase = false;

            for (int i = 0; i < password.Length - 1; i++)
            {
                if (LowerCaseLetters.Contains(password[i]))
                {
                    ContainLowerCase = true;
                }

            }

            if (!ContainLowerCase)
            {
                errors.Add(new IdentityError
                {
                    Description = "Пароль должен содержать как минимум одну строчную букву"
                });

            }

            bool ContainUpperCase = false;

            for (int i = 0; i < password.Length - 1; i++)
            {
                if (LowerCaseLetters.ToUpper().Contains(password[i]))
                {
                    ContainUpperCase = true;
                }

            }

            if (!ContainUpperCase)
            {
                errors.Add(new IdentityError
                {
                    Description = "Пароль должен содержать как минимум одну заглавную букву"
                });

            }

            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray())); 
            //return Task.FromResult(IdentityResult.Success);
        }
    }
}
