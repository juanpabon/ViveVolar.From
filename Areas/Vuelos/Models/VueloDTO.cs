using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViveVolar.From.Areas.Vuelos.Models
{
    public class VueloDTO
    {
        public int Id { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaLlegada { get; set; }
        public int CantidadSillas { get; set; }
        public string CiudadOrigen { get; set; }
        public string CiudadDestino { get; set; }
        public double Precio { get; set; }
        public string TipoVuelo { get; set; }
    }
}
