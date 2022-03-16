using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_application_of_students_squad_SFEDU.ViewModels
{
    // Test
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле <Пароль> не должно быть пустым!")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Поле <Подтвердить пароль> не должно быть пустым!")]
        public string OldPassword { get; set; }
    }
}
