using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Threading;

namespace ApiTeht
{
    public class CustomMiddleware : Controller
    {
        IConfiguration _iconfiguration;
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next, IConfiguration iconfiguration){
            _iconfiguration = iconfiguration;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext){
            //Tarkista onko koodi oikea
            string key = _iconfiguration.GetSection("Data").GetSection("API-Key").Value;
            StringValues header_key;
            if(httpContext.Request.Headers.TryGetValue("x-api-key", out header_key)){
                if(key != header_key){
                    
                    httpContext.Response.StatusCode = 403;
                }  
                else{
                    StringValues admin_header;
                    
                    if(httpContext.Request.Headers.TryGetValue("AdminNumber", out admin_header)){
                        var identity = (ClaimsIdentity)httpContext.User.Identity;
                        identity.AddClaim(new Claim("AdminNumber", admin_header));
                    }
                    httpContext.Response.StatusCode = 200;
                    await _next.Invoke(httpContext);
                }             
            }
            else{

                httpContext.Response.StatusCode = 400;
            }
        }
    }

    public static class CustomMiddlewareExtensions{
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder){
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}