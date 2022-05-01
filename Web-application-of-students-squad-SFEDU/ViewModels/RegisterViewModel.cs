using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Web_application_of_students_squad_SFEDU.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле <Email> не должно быть пустым!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле <Пароль> не должно быть пустым!")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.",MinimumLength = 5)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле <Подтвердить пароль> не должно быть пустым!")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

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
