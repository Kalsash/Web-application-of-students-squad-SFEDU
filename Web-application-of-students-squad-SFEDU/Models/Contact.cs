using System;
using System.ComponentModel.DataAnnotations;

namespace Web_application_of_students_squad_SFEDU.Models
{
    public class Contact
    {
        //свойство Id будет служить первичным ключом в соответствующей таблице в БД
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Адрес электронной почты обязателен!")]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

        [Display(Name = "Полное имя")]
        public string FullName { get; set; }

        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Текстовое сообщение")]
        public string TextMessage { get; set; }
    }
}
