using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViveVolar.From.Areas.Reservas.Models
{
    public class RespuestaReserva
    {
        public DateTime FechaReserva { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; }
        public double PrecioReserva { get; set; }
        public string CodigoReserva { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaLlegada { get; set; }
        public string CiudadOrigen { get; set; }
        public string CiudadDestino { get; set; }
        public string TipoVuelo { get; set; }
        public string Nombre { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }
}
