using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string DirectionOfSquad { get; set; } // Направление отряда
        public string NameOfSquad { get; set; } // Название отряда
        public string Department { get; set; } // Факультет

        [Required(ErrorMessage = "Поле <Дата рождения> является обязательным!")]
        public string BirthDate { get; set; } // Дата рождения
        public string Course { get; set; } // Курс
        public string Group { get; set; } // Группа
        public string Money { get; set; } // Бюджет или коммерция
        public string VK { get; set; } // Ссылка вк
    }
}