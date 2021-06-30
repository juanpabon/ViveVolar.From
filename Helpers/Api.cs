using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ViveVolar.From.Helpers
{
    public class Api
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://vivevolarwebapi20210628160511.azurewebsites.net/");
            return client;
        }
    }
}
