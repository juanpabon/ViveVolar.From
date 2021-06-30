using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViveVolar.From.Areas.Vuelos.Models
{
    public class BuscarVueloDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public int cantidadPasajeros { get; set; }
    }
}
