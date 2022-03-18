using System;
using System.ComponentModel.DataAnnotations;

namespace Web_application_of_students_squad_SFEDU.Models
{
    public class Article
    {
        //свойство Id будет служить первичным ключом в соответствующей таблице в БД
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Заполните название")]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Содержание")]
        public string Text { get; set; }

        [Display(Name = "Титульная картинка")]
        public virtual string TitleImagePath { get; set; }

        [Display(Name = "Дата и время")]
        public DateTime Data { get; set; }
    }
}
