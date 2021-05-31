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
        public static int CalculateAge(this DateTime dateTime)
        {
            var age = DateTime.Today.Year - dateTime.Year;
            if (dateTime.AddYears(age) > DateTime.Today) age--;
            return age;
        }
    }
}
