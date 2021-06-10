using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HittaPartnerApp.API.Helpers
{
    public static class Extentions
    {
        public static void AddPagination (this HttpResponse respons,int currentPage,int itemPerPage,int totalItem,int totalPage)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemPerPage, totalItem, totalPage);
            respons.Headers.Add("paginationHeader", JsonConvert.SerializeObject(paginationHeader));
            respons.Headers.Add("Access-Control-Expose-Header", "Pagination");
        }
        public static int CalculateAge(this DateTime dateTime)
        {
            var age = DateTime.Today.Year - dateTime.Year;
            if (dateTime.AddYears(age) > DateTime.Today) age--;
            return age;
        }
    }
}
