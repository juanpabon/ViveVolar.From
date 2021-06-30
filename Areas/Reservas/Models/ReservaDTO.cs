using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViveVolar.From.Areas.Reservas.Models
{
    public class ReservaDTO
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public double PrecioReserva { get; set; }
        public int VueloId { get; set; }
        public ClienteDTO CLiente { get; set; }
    }
    public class ClienteDTO
    {
        public string Nombre { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }
}
