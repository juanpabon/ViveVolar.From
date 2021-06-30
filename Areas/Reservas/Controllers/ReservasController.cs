using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ViveVolar.From.Areas.Reservas.Models;
using ViveVolar.From.Helpers;

namespace ViveVolar.From.Areas.Reservas.Controllers
{
    [Area("Reservas")]
    public class ReservasController : Controller
    {
        private Api _api = new Api();
        private static InputModel _dataInput;
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelRegister
        {

        }
        public async Task<IActionResult> DetailReserva(int id, BuscarReservaDTO dto)
        {
            HttpClient client = _api.Initial();

            var informacionAMandar = Newtonsoft.Json.JsonConvert.SerializeObject(dto,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromMinutes(10);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/reserva/search/");
            request.Content = new StringContent(informacionAMandar, Encoding.UTF8, "application/json");//CONTENT-TYPE header

            HttpResponseMessage result = await client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                var results = result.Content.ReadAsStringAsync().Result;
                Respuesta respuesta = new Respuesta();
                respuesta = JsonConvert.DeserializeObject<Respuesta>(results);
                if (respuesta.Exito == 1)
                {
                    InputModelRegister data = new InputModelRegister();
                    string obj = respuesta.Data.ToString();

                    var str = JsonConvert.SerializeObject(data);
                    HttpContext.Session.Set("DetailReserva", Encoding.UTF8.GetBytes(str));

                    data = JsonConvert.DeserializeObject<InputModelRegister>(obj);
                    return View(data);
                }
                TempData["message"] = respuesta.Mensaje;
                return View();
            }
            TempData["message"] = "Error llamando el servicio";
            return View();
        }
        public async Task<IActionResult> Pay(int id)
        {
            HttpClient client = _api.Initial();
            PagarReservaDTO dto = new();
            dto.Id = id;
            var informacionAMandar = Newtonsoft.Json.JsonConvert.SerializeObject(dto,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromMinutes(10);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "api/reserva/pay/");
            request.Content = new StringContent(informacionAMandar, Encoding.UTF8, "application/json");//CONTENT-TYPE header

            HttpResponseMessage result = await client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                var results = result.Content.ReadAsStringAsync().Result;
                Respuesta respuesta = new Respuesta();
                respuesta = JsonConvert.DeserializeObject<Respuesta>(results);
                if (respuesta.Exito == 1)
                {
                    TempData["message"] = "Hemos recibido el pago correctamente";
                    return View();
                }
                TempData["message"] = respuesta.Mensaje;
                return View();
            }
            TempData["message"] = "Error llamando el servicio";
            return View();
        }
    }
}
