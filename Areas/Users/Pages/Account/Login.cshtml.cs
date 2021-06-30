using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ViveVolar.From.Areas.Users.Models;
using ViveVolar.From.Helpers;

namespace ViveVolar.From.Areas.Users.Pages.Account
{
    public class LoginModel : PageModel
    {
        private static InputModel _dataInput;
        private Api _api = new Api();
        private AdminSession _adminSession;
        private UserSession _userSession;
        public void OnGet()
        {
            if (_dataInput != null)
            {
                Input = _dataInput;
            }
            _adminSession = new AdminSession();
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelLogin
        {

        }
        public async Task<IActionResult> OnPost()
        {
            if (await SaveAsync())
            {
                return Redirect("/Vuelos/Vuelos?area=Vuelos");
            }
            else
            {
                return Redirect("/Users/Register");
            }
        }
        private async Task<bool> SaveAsync()
        {
            _dataInput = Input;
            bool valor=false;
            if (ModelState.IsValid)
            {
                try
                {
                    UserDTO _user = new UserDTO();
                    _user.Login = Input.Login;
                    _user.Password = Input.Password;
                    HttpClient client = _api.Initial();
                    var informacionAMandar = Newtonsoft.Json.JsonConvert.SerializeObject(_user,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromMinutes(10);

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/usuario/login/");
                    request.Content = new StringContent(informacionAMandar, Encoding.UTF8, "application/json");//CONTENT-TYPE header
                    HttpResponseMessage result = await client.SendAsync(request);

                    if (result.IsSuccessStatusCode)
                    {
                        var results = result.Content.ReadAsStringAsync().Result;
                        Respuesta respuesta = new Respuesta();
                        respuesta = JsonConvert.DeserializeObject<Respuesta>(results);
                        if (respuesta.Exito == 1)
                        {
                            
                            UserSession userSession = new UserSession();
                            string  obj = respuesta.Data.ToString();
                            userSession = JsonConvert.DeserializeObject<UserSession>(obj);

                            userSession.Login = Input.Login;
                            userSession.Rol = 1;
                            
                            var str = JsonConvert.SerializeObject(userSession);
                            HttpContext.Session.Set("UserLogin", Encoding.UTF8.GetBytes(str));
                            
                            _dataInput = null;
                            valor = true;
                        }
                        else
                        {
                            _dataInput.ErrorMessage = respuesta.Mensaje;
                            valor = false;
                            HttpContext.Session.Remove("UserLogin");
                        }
                    }
                    else
                    {
                        HttpContext.Session.Remove("UserLogin");
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
