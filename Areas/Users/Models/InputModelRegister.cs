using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViveVolar.From.Areas.Users.Models
{
    public class InputModelRegister
    {
        [Required(ErrorMessage = "El campo login es obligatorio.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "El campo password es obligatorio.")]
        [StringLength(9,ErrorMessage ="El número de caracteres de {0} debe ser al menos {2}.",MinimumLength =5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
    }
}
