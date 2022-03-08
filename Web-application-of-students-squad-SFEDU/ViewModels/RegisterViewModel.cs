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

        [Required]
        [Display(Name = "Год рождения")]
        public int Year { get; set; }

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
    }
}
