using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
namespace PublicApi.MiddleWare
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();
            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var username = resultContext.HttpContext.User.GetuserName();
            var repo = resultContext.HttpContext.RequestServices.GetService<IuserExtend>();
            var user = await repo.GetuserAsync(null, username);
            user.clsUsers.LastActive = DateTime.Now;
            await repo.SaveAllChanges();
        }
    }
}
