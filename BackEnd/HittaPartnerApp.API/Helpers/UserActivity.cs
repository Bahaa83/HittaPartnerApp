using HittaPartnerApp.API.Services.IRepositories;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace HittaPartnerApp.API.Helpers
{
    public class UserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();
            var userId = resultContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var repo = resultContext.HttpContext.RequestServices.GetService<IHittaPartnerRepo>();
            var currentUser = await repo.GetUserByID(userId);
            currentUser.LastActive = DateTime.Now;
            await repo.SaveAll();
        }
    }
}
