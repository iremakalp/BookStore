using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        // gelen request ve response'lar loglanmak isteniyor
        // bir middleware http cagrimi geldiginde http requeste karsilik gelen response donene kadar ki islemlerin loglanmasi
        public async Task Invoke(HttpContext context)
        {
            // path endpoint bilgisi
            // endpoint
            string message="[Request] HTTP" + context.Request.Method + " - " + context.Request.Path;
            Console.WriteLine(message);
            await _next(context);

            // response
            message = "[Response] HTTP" + context.Response.StatusCode;
            Console.WriteLine(message);

        }


    }

    public static class CustomExceptionMiddlewareExtensions
    {
        // startup icerisinden ulasilacak isim UseCustomExceptionMiddleware
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}