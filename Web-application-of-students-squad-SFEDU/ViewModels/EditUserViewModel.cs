using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_application_of_students_squad_SFEDU.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; } // Фамилия
        public string Name { get; set; } // Имя
        public string Patronymic { get; set; } // Отчество
        public string DirectionOfSquad { get; set; } // Направление отяда
        public string Department { get; set; } // Факультет
        public string BirthDate { get; set; } // Дата рождения
        public int Course { get; set; } // Курс
        public int Group { get; set; } // Группа
        public string Money { get; set; } // Бюджет или коммерция
        public string VK { get; set; } // Ссылка вк
    }
}