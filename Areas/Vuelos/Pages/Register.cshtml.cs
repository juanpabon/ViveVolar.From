using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ViveVolar.From.Areas.Vuelos.Models;
using ViveVolar.From.Helpers;
using static ViveVolar.From.Areas.Users.Pages.Account.LoginModel;

namespace ViveVolar.From.Areas.Vuelos.Pages
{
    public class RegisterModel : PageModel
    {
        private static InputModel _dataInput;
        private Api _api = new Api();
        private UserSession _userSession;
        public void OnGet()
        {
            string data = HttpContext.Session.GetString("UserLogin");
            if (!string.IsNullOrWhiteSpace(data))
            {
                _userSession = new UserSession();
                _userSession = JsonConvert.DeserializeObject<UserSession>(data);
            }

            if (_dataInput != null)
            {
                Input = _dataInput;

            }
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelRegister
        {

        }
        public async Task<IActionResult> OnPost()
        {
            string data = HttpContext.Session.GetString("UserLogin");
            if (!string.IsNullOrWhiteSpace(data))
            {
                _userSession = new UserSession();
                _userSession = JsonConvert.DeserializeObject<UserSession>(data);
            }
            if (_userSession != null)
            {
                if (_userSession.Rol == 1)
                {
                    if (await SaveAsync())
                    {
                        return Redirect("/Vuelos/Vuelos?area=Vuelos");
                    }
                    else
                    {
                        return Redirect("/Vuelos/Register");
                    }
                }

            }
            return Redirect("/Users/Login");
        }
        //[Authorize(Roles = "Admin")]
        private async Task<bool> SaveAsync()
        {

            _dataInput = Input;
            bool valor;
            if (ModelState.IsValid)
            {
                try
                {
                    string data = HttpContext.Session.GetString("UserLogin");
                    if (!string.IsNullOrWhiteSpace(data))
                    {
                        _userSession = new UserSession();
                        _userSession = JsonConvert.DeserializeObject<UserSession>(data);
                    }


                    VueloDTO _vuelo = new VueloDTO();
                    _vuelo.FechaSalida = Input.FechaSalida;
                    _vuelo.FechaLlegada = Input.FechaLlegada;
                    _vuelo.CantidadSillas = Input.CantidadSillas;
                    _vuelo.CiudadOrigen = Input.CiudadOrigen;
                    _vuelo.CiudadDestino = Input.CiudadDestino;
                    _vuelo.Precio = Input.Precio;
                    _vuelo.TipoVuelo = Input.TipoVuelo;


                    HttpClient client = _api.Initial();
                    var informacionAMandar = Newtonsoft.Json.JsonConvert.SerializeObject(_vuelo,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });




                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _userSession.Token);
                    client.Timeout = TimeSpan.FromMinutes(10);


                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/vuelo/register/");
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
                        if (result.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            HttpContext.Session.Remove("UserLogin");
                        }
                        _dataInput.ErrorMessage = "Error llamando al servicio";
                        valor = false;

                    }
                }
                catch (Exception ex)
                {
                    _dataInput.ErrorMessage = ex.Message;
                    valor = false;
                }
                //_dataInput.ErrorMessage = $"El {Input.Email} ya esta registrado";
                //valor = false;
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
