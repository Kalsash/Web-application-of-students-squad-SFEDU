using System;
using System.ComponentModel.DataAnnotations;

namespace Web_application_of_students_squad_SFEDU.Models
{
    public class Join
    {
        //свойство Id будет служить первичным ключом в соответствующей таблице в БД
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Поле <Фамилия> является обязательным!")]
        [Display(Name = "Фамилия")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "Поле <Имя> является обязательным!")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Нужно указать номер телефона!")]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Display(Name = "Пол")]
        public string Gender { get; set; }

        [Display(Name = "Направление отряда")]
        public string DirectionOfSquad { get; set; }

        [Display(Name = "Факультет")]
        public string Department { get; set; }
    }
}
