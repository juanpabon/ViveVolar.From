using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViveVolar.From.Helpers
{
    public class AdminSession
    {
        public bool SetSession(string Key,string obj,HttpContext context)
        {
            try
            {
                if (context.Session.Keys.Contains(Key))
                {
                    RemoveSession(Key, context);
                }
                context.Session.Set(Key, Encoding.UTF8.GetBytes(obj));
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string GetSession(string Key, HttpContext context)
        {
            try
            {
                return context.Session.GetString(Key);
               
            }
            catch
            {
                return null;
            }
        }
        public bool RemoveSession(string Key, HttpContext context)
        {
            try
            {
                context.Session.Remove(Key);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
