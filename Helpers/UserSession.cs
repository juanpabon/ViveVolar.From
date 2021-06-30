using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViveVolar.From.Helpers
{
    public class UserSession
    {
        public string Login { get; set; }
        public string Token { get; set; }
        public int Success { get; set; }
        public int Rol { get; set; }
    }
}
