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
        [Required(ErrorMessage = "Поле <Фамилия> является обязательным!")]
        public string Surname { get; set; } // Фамилия
        [Required(ErrorMessage = "Поле <Имя> является обязательным!")]
        public string Name { get; set; } // Имя
        [Required(ErrorMessage = "Поле <Отчество> является обязательным!")]
        public string Patronymic { get; set; } // Отчество
        public string DirectionOfSquad { get; set; } // Направление отряда
        [Required(ErrorMessage = "Поле <Название отряда> является обязательным!")]
        public string NameOfSquad { get; set; } // Название отряда
        [Required(ErrorMessage = "Поле <Факультет> является обязательным!")]
        public string Department { get; set; } // Факультет

        [Required(ErrorMessage = "Поле <Дата рождения> является обязательным!")]
        public string BirthDate { get; set; } // Дата рождения
        [Required(ErrorMessage = "Поле <Курс> является обязательным!")]
        public string Course { get; set; } // Курс
        [Required(ErrorMessage = "Поле <Группа> является обязательным!")]
        public string Group { get; set; } // Группа
        public string Money { get; set; } // Бюджет или коммерция
        public string VK { get; set; } // Ссылка вк
    }
}