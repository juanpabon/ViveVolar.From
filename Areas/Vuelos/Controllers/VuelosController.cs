using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ViveVolar.From.Areas.Vuelos.Models;
using ViveVolar.From.Helpers;
using ViveVolar.From.Library;
using ViveVolar.From.Models;

namespace ViveVolar.From.Areas.Vuelos.Controllers
{
    [Area("Vuelos")]
    public class VuelosController : Controller
    {
        private static DataPaginador<InputModelRegister> models;
        private Api _api = new Api();
        public async Task<IActionResult> Vuelos( int id,string filtrar)
        {
            Object[] objects = new Object[3];

            HttpClient client = _api.Initial();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromMinutes(10);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/vuelo/getall/");
            
            HttpResponseMessage result = await client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                var results = result.Content.ReadAsStringAsync().Result;
                Respuesta respuesta = new Respuesta();
                respuesta = JsonConvert.DeserializeObject<Respuesta>(results);
                if (respuesta.Exito == 1)
                {
                    List<InputModelRegister> data = new List<InputModelRegister>();
                    string obj = respuesta.Data.ToString();
                    HttpContext.Session.Set("GetVuelos", Encoding.UTF8.GetBytes(obj));

                    data = JsonConvert.DeserializeObject<List<InputModelRegister>>(obj);

                    if(!string.IsNullOrEmpty(filtrar))
                    {
                        List<InputModelRegister> order = new List<InputModelRegister>();
                        order = data.Where(x => x.CiudadOrigen.StartsWith(filtrar) || x.CiudadDestino.StartsWith(filtrar) || x.FechaSalida.ToString().StartsWith(filtrar)).ToList();
                        data = order;
                    }

                    if (0 < data.Count)
                    {
                        var url = Request.Scheme + "://" + Request.Host.Value;
                        objects = new LPaginador<InputModelRegister>().paginador(data,
                            id, 10, "Vuelos", "Vuelos", "Vuelos", url);
                    }
                    else
                    {
                        objects[0] = "No hay datos que mostrar";
                        objects[1] = "No hay datos que mostrar";
                        objects[2] = new List<InputModelRegister>();
                    }
                    models = new DataPaginador<InputModelRegister>
                    {
                        List = (List<InputModelRegister>)objects[2],
                        Pagi_info = (String)objects[0],
                        Pagi_navegacion = (String)objects[1],
                        Input = new InputModelRegister(),
                    };
                    return View(models);
                }
            }
            objects[0] = "No hay datos que mostrar";
            objects[1] = "No hay datos que mostrar";
            objects[2] = new List<InputModelRegister>();
        
            models = new DataPaginador<InputModelRegister>
                    {
                        List = (List<InputModelRegister>) objects[2],
                        Pagi_info = (String)objects[0],
                        Pagi_navegacion = (String)objects[1],
                        Input = new InputModelRegister()
            };
            return View(models);
        }
        public async Task<IActionResult> SearchClient(int id,BuscarVueloDTO dto)
        {
            Object[] objects = new Object[3];
            
            DatoReserva dtoB = new();
            dtoB.cantidadPasajeros = dto.cantidadPasajeros;
            
            var str = JsonConvert.SerializeObject(dtoB);
            HttpContext.Session.Set("DatoReserva", Encoding.UTF8.GetBytes(str));

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

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/vuelo/search/");
            request.Content = new StringContent(informacionAMandar, Encoding.UTF8, "application/json");//CONTENT-TYPE header

            HttpResponseMessage result = await client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                var results = result.Content.ReadAsStringAsync().Result;
                Respuesta respuesta = new Respuesta();
                respuesta = JsonConvert.DeserializeObject<Respuesta>(results);
                if (respuesta.Exito == 1)
                {
                    List<InputModelRegister> data = new List<InputModelRegister>();
                    string obj = respuesta.Data.ToString();
                    HttpContext.Session.Set("VueloDisponible", Encoding.UTF8.GetBytes(obj));

                    data = JsonConvert.DeserializeObject<List<InputModelRegister>>(obj);

                    if (0 < data.Count)
                    {
                        var url = Request.Scheme + "://" + Request.Host.Value;
                        objects = new LPaginador<InputModelRegister>().paginador(data,
                            id, 10, "Vuelos", "Vuelos", "Vuelos", url);
                    }
                    else
                    {
                        objects[0] = "No hay datos que mostrar";
                        objects[1] = "No hay datos que mostrar";
                        objects[2] = new List<InputModelRegister>();

                        TempData["message"] = "No se encontraron vuelos";
                        return Redirect("/Vuelos/SearchFlights");
                    }
                    models = new DataPaginador<InputModelRegister>
                    {
                        List = (List<InputModelRegister>)objects[2],
                        Pagi_info = (String)objects[0],
                        Pagi_navegacion = (String)objects[1],
                        Input = new InputModelRegister(),
                    };
                    return View(models);
                }
                TempData["message"] = respuesta.Mensaje;
                return Redirect("/Vuelos/SearchFlights");
            }
            TempData["message"] = "Error llamando el servicio";
            return Redirect("/Vuelos/SearchFlights");
        }


    }
}
