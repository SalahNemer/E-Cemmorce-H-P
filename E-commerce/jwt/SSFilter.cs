using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace asp_core_3.auth.Filter;

public class SSFilter : IAsyncActionFilter
{
    private readonly List<string> _publicPaths;

    public SSFilter()
    {
        _publicPaths = new List<string>
        {
            "/api/auth/login" ,
            "/api/GoogleLogin/google"
        };
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var requestPath = context.HttpContext.Request.Path.Value;
        Console.WriteLine("Requested Path : " + requestPath);
        if (_publicPaths.Contains(requestPath))
        {
            await next();
            return;
        }
        var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var claimsPrincipal = context.HttpContext.User;

            if (claimsPrincipal?.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = claimsPrincipal.FindFirst("user_id")?.Value;
                var exp = claimsPrincipal.FindFirst("exp")?.Value;
                Console.WriteLine("User ID: " + userIdClaim);

                
                
         
                 Console.WriteLine("Exp: " + exp);
                long current  =  DateTimeToUnixTimeMilliseconds(DateTime.Now);
                Console.WriteLine("Current Time: " + current);
                 
                if (current> Convert.ToInt32(exp))
                {
                    context.Result = new ForbidResult(); 
                    return; 
                }
            

                if (true)
                {

                }
                else
                {
                    context.Result = new UnauthorizedResult(); 
                    return; 
                }
            }
            else
            {
                context.Result = new ForbidResult(); 
                return; 
            }
        }
        else
        {
            context.Result = new UnauthorizedResult(); 
            return;
        }

        await next();
    }
  public   long DateTimeToUnixTimeMilliseconds(DateTime dateTime)
    {
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        TimeSpan difference = dateTime.ToUniversalTime() - epoch;

        return (long)difference.TotalMilliseconds/1000;


}



}