using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViveVolar.From.Areas.Reservas.Models;
using ViveVolar.From.Helpers;

namespace ViveVolar.From.Areas.Reservas.Pages
{
    public class SearchReservaModel : PageModel
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
                BuscarReservaDTO dto = new();
                dto.Nombre = Input.Nombre;
                dto.CodigoReserva = Input.CodigoReserva;
                return RedirectToAction("DetailReserva", "Reservas", dto);
            }
            return Redirect("/Reservas/SearchReserva");
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
