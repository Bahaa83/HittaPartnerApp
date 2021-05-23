using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HittaPartnerApp.API.Helpers
{
    public static class Extentions
    {
        public static void AddApplicationError (this HttpResponse respons,string message)
        {
            respons.Headers.Add("Application-Error", message);
            respons.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            respons.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}
