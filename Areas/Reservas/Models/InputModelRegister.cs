using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViveVolar.From.Areas.Reservas.Models
{
    public class InputModelRegister
    {

        [Display(Name = "Fecha de salida")]
        [DataType(DataType.DateTime)]
        public DateTime FechaSalida { get; set; }

        [Display(Name = "Fecha de llegada")]
        [DataType(DataType.DateTime)]
        public DateTime FechaLlegada { get; set; }

        [Display(Name = "Ciudad de origen")]
        public string CiudadOrigen { get; set; }

        [Display(Name = "Ciudad destino")]
        public string CiudadDestino { get; set; }

        [DataType(DataType.Currency)]
        public double Precio { get; set; }

        [Display(Name = "Tipo Vuelo")]
        public string TipoVuelo { get; set; }


        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }
        
        [Display(Name = "Precio reserva")]
        public double PrecioReserva { get; set; }

        [Display(Name = "Fecha reserva")]
        public DateTime FechaReserva { get; set; }

        [Display(Name = "Estado reserva")]
        public string Estado { get; set; }

        [Display(Name = "Codigo reserva")]
        public string CodigoReserva { get; set; }

        [Display(Name = "Fecha pago reserva")]
        public DateTime FechaPago { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Display(Name = "Tipo identificación")]
        public string TipoIdentificacion { get; set; }

        [Display(Name = "Numero de identificación")]
        public string Identificacion { get; set; }

        [Display(Name = "Correo electronico")]
        [Required(ErrorMessage = "El correo electronico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es una dirección de correo electrónico válida.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        public string Direccion { get; set; }


        public int VueloId { get; set; }
        public int Id { get; set; }
        public ClienteDTO CLiente { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
    }
}
