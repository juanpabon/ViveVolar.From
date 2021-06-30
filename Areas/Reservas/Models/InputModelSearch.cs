using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViveVolar.From.Areas.Reservas.Models
{
    public class InputModelSearch
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Display(Name = "Codigo reserva")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string CodigoReserva { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
    }
}
