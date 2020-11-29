using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStoreCoreApplication.Domain.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Имя является обязательным")]
        [Display(Name = "Имя")]
        [StringLength(maximumLength: 200, MinimumLength = 2, ErrorMessage = "В имени должно быть не менее 2х и не более 200 символов")]
        public string IName { get; set;}

        [Required(AllowEmptyStrings = false, ErrorMessage = "Фамилия является обязательной")]
        [Display(Name = "Фамилия")]
        public string FName { get; set;}

        [Display(Name = "Отчество")]
        public string OName { get; set;}


        public int Age { get; set;}

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Дата начала трудового договора")]
        [DataType(DataType.DateTime)]
        public DateTime EmployementDate { get; set; }

    }
}
