using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViveVolar.From.Areas.Vuelos.Models
{
    public class InputModelRegister
    {
        [Display(Name = "Fecha de salida")]
        [Required(ErrorMessage = "El campo Fecha Salida es obligatorio.")]
        [DataType(DataType.DateTime)]
        public DateTime FechaSalida { get; set; }
        
        [Display(Name = "Fecha de llegada")]
        [Required(ErrorMessage = "El campo Fecha Llegada es obligatorio.")]
        [DataType(DataType.DateTime)]
        public DateTime FechaLlegada { get; set; }

        [Display(Name = "Cantidad de sillas")]
        [Required(ErrorMessage = "El campo cantidad de sillas es obligatorio.")]
        public int CantidadSillas { get; set; }

        [Required(ErrorMessage = "El campo Ciudad de Origen es obligatorio.")]
        [Display(Name = "Ciudad de origen")]
        public string CiudadOrigen { get; set; }
        
        [Required(ErrorMessage = "El campo Ciudad destino es obligatorio.")]
        [Display(Name = "Ciudad destino")]
        public string CiudadDestino { get; set; }
        
        [Required(ErrorMessage = "El campo precio es obligatorio.")]
        [DataType(DataType.Currency)]
        public double Precio { get; set; }
        
        [Required(ErrorMessage = "El campo Tipo de vuelo es obligatorio.")]
        [Display(Name = "Tipo Vuelo")]
        public string TipoVuelo { get; set; }
        public int Id { get; set; }
        public int sillasDisponibles { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
    }
}
