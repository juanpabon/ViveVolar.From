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
using ViveVolar.From.Areas.Reservas.Models;
using ViveVolar.From.Areas.Vuelos.Models;
using ViveVolar.From.Helpers;

namespace ViveVolar.From.Areas.Reservas.Pages
{
    public class RegisterModel : PageModel
    {
        private static InputModel _dataInput;
        private Api _api = new Api();
        public void OnGet(int id)
        {
            if (_dataInput != null)
            {
                Input = _dataInput;

            }

            DatoReserva dtoBuscar = new();
            string obj = HttpContext.Session.GetString("DatoReserva");
            dtoBuscar = JsonConvert.DeserializeObject<DatoReserva>(obj);
            if (id > 0)
            {
                dtoBuscar.VueloId = id;
                var str = JsonConvert.SerializeObject(dtoBuscar);
                HttpContext.Session.Set("DatoReserva", Encoding.UTF8.GetBytes(str));
            }

            List<Vuelos.Models.InputModelRegister> vuelos = new();
            string obj2 = HttpContext.Session.GetString("VueloDisponible");
            vuelos = JsonConvert.DeserializeObject<List<Vuelos.Models.InputModelRegister>>(obj2);

            Vuelos.Models.InputModelRegister dto = new();
            dto = vuelos.Where(x => x.Id == id).FirstOrDefault();
            Input = new InputModel
            {
                Cantidad = dtoBuscar.cantidadPasajeros,
                CiudadDestino = dto.CiudadDestino,
                CiudadOrigen = dto.CiudadOrigen,
                FechaLlegada = dto.FechaLlegada,
                FechaSalida = dto.FechaSalida,
                Precio = dto.Precio,
                PrecioReserva = dto.Precio * dtoBuscar.cantidadPasajeros,
                TipoVuelo = dto.TipoVuelo,
                VueloId = dto.Id,
            };
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : Models.InputModelRegister
        {

        }
        public async Task<IActionResult> OnPost()
        {
            if (await SaveAsync())
            {
                return Redirect("/Vuelos/SearchFlights");
            }
            else
            {
                DatoReserva dtoBuscar = new();
                string obj = HttpContext.Session.GetString("DatoReserva");
                dtoBuscar = JsonConvert.DeserializeObject<DatoReserva>(obj);
                return Redirect("/Reserva/Register?id="+ dtoBuscar.VueloId.ToString());
            }
        }
        private async Task<bool> SaveAsync()
        {
            _dataInput = Input;
            bool valor;
            if (ModelState.IsValid)
            {
                try
                {
                    ClienteDTO _cliente = new();
                    _cliente.Direccion = Input.Direccion;
                    _cliente.Email = Input.Email;
                    _cliente.Identificacion = Input.Identificacion;
                    _cliente.Nombre = Input.Nombre;
                    _cliente.Telefono = Input.Nombre;
                    _cliente.TipoIdentificacion = Input.TipoIdentificacion;

                    ReservaDTO _reserva = new ReservaDTO();
                    _reserva.Cantidad = Input.Cantidad;
                    _reserva.CLiente = _cliente;
                    _reserva.PrecioReserva = Input.PrecioReserva;
                    _reserva.VueloId = Input.VueloId;


                    HttpClient client = _api.Initial();
                    var informacionAMandar = Newtonsoft.Json.JsonConvert.SerializeObject(_reserva,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });




                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    
                    client.Timeout = TimeSpan.FromMinutes(10);


                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/reserva/register/");
                    request.Content = new StringContent(informacionAMandar, Encoding.UTF8, "application/json");//CONTENT-TYPE header
                    HttpResponseMessage result = await client.SendAsync(request);
                    //HttpResponseMessage result = await client.GetAsync("api/usuario/login/");
                    if (result.IsSuccessStatusCode)
                    {
                        var results = result.Content.ReadAsStringAsync().Result;
                        Respuesta respuesta = new Respuesta();
                        respuesta = JsonConvert.DeserializeObject<Respuesta>(results);
                        if (respuesta.Exito == 1)
                        {
                            _dataInput = null;
                            valor = true;
                        }
                        else
                        {
                            _dataInput.ErrorMessage = respuesta.Mensaje;
                            valor = false;
                        }
                    }
                    else
                    {
                        _dataInput.ErrorMessage = "Error llamando al servicio";
                        valor = false;

                    }
                }
                catch (Exception ex)
                {
                    _dataInput.ErrorMessage = ex.Message;
                    valor = false;
                }
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _dataInput.ErrorMessage += error.ErrorMessage;
                    }
                }
                valor = false;
            }

            return valor;
        }
    }
}
