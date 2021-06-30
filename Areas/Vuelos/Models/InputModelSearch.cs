using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViveVolar.From.Areas.Vuelos.Models
{
    public class InputModelSearch
    {
        [Display(Name = "Fecha desde")]
        [Required(ErrorMessage = "El campo Fecha desde es obligatorio.")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; } = DateTime.Now;

        [Display(Name = "Fecha hasta")]
        [Required(ErrorMessage = "El campo Fecha hasta es obligatorio.")]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; } = DateTime.Now.AddDays(1);

[       Display(Name = "Cantidad de pasajeros")]
        [Required(ErrorMessage = "El campo cantidad de pasajeros es obligatorio.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El campo Ciudad de Origen es obligatorio.")]
        [Display(Name = "Ciudad de origen")]
        public string CiudadOrigen { get; set; }

        [Required(ErrorMessage = "El campo Ciudad destino es obligatorio.")]
        [Display(Name = "Ciudad destino")]
        public string CiudadDestino { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
    }
}
