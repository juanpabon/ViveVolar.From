using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ViveVolar.From.Areas.Vuelos.Models;
using ViveVolar.From.Helpers;
using ViveVolar.From.Library;
using ViveVolar.From.Models;

namespace ViveVolar.From.Areas.Vuelos.Pages
{
    public class SearchFlightsModel : PageModel
    {
        private static InputModel _dataInput;
        private Api _api = new Api();
        public void OnGet()
        {
            if (_dataInput != null)
            {
                Input = _dataInput;
            }
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelSearch
        {
           
        }
        public IActionResult OnPost()
        {
            if (Validate())
            {
                BuscarVueloDTO dto = new BuscarVueloDTO();
                dto.cantidadPasajeros = Input.Cantidad;
                dto.Destino = Input.CiudadDestino;
                dto.Origen = Input.CiudadOrigen;
                dto.FechaInicio = Input.FechaInicio;
                dto.FechaFin = Input.FechaFin;
                return RedirectToAction("SearchClient", "Vuelos", dto);
            }
            return Redirect("/Vuelos/SearchFlights");
        }
        private bool Validate()
        {
            _dataInput = Input;
            if (!ModelState.IsValid)
            {
                _dataInput.ErrorMessage = "Todos los campos son requeridos";
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
