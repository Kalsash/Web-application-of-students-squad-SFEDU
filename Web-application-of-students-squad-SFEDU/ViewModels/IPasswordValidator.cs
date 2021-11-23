using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_application_of_students_squad_SFEDU.ViewModels
{
    public interface IPasswordValidator<T> where T : class
    {

        Task<IdentityResult> ValidateAsync(UserManager<T> manager, T user, string password);
    }
}
