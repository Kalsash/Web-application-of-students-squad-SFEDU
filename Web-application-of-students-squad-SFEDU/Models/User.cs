using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_application_of_students_squad_SFEDU.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}
