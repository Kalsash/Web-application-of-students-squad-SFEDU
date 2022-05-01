using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_application_of_students_squad_SFEDU.Models
{
    public class User : IdentityUser
    {
        public string Surname { get; set; } // Фамилия
        public string Name { get; set; } // Имя
        public string Patronymic { get; set; } // Отчество
        public string DirectionOfSquad { get; set; } // Направление отряда
        public string NameOfSquad { get; set; } // Название отряда
        public string Department { get; set; } // Факультет
        public string BirthDate { get; set; } // Дата рождения
        public string Course { get; set; } // Курс
        public string Group { get; set; } // Группа
        public string Money { get; set; } // Бюджет или коммерция
        public string VK { get; set; } // Ссылка вк
    }
}