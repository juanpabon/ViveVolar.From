using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ViveVolar.From.Areas.Vuelos.Models;
using ViveVolar.From.Helpers;

namespace ViveVolar.From.Areas.Vuelos.Pages
{
    public class DetailsModel : PageModel
    {
        private Api _api = new Api();
        public void OnGet(int id)
        {
            List<InputModelRegister> data = new List<InputModelRegister>();
            string obj = HttpContext.Session.GetString("GetVuelos");
            data = JsonConvert.DeserializeObject<List<InputModelRegister>>(obj);

            if (data != null)
            {
                InputModelRegister _vuelo = new InputModelRegister();
                _vuelo = data.Where(x => x.Id == id).FirstOrDefault();
                if (_vuelo != null)
                {
                    Input = new InputModel
                    {
                        DataVuelo = _vuelo,
                    };
                }
            }
               
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            public InputModelRegister DataVuelo { get; set; }
        }
    }
}
