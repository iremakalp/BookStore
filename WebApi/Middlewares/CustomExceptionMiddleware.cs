using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        ILoggerService _loggerService;
        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        // gelen request ve response'lar loglanmak isteniyor
        // bir middleware http cagrimi geldiginde http requeste karsilik gelen response donene kadar ki islemlerin loglanmasi
        public async Task Invoke(HttpContext context)
        {
            // ne kadar surede dondugunu bulmak icin
            // diagnostics sistemin sagligini olcen, uzerindeki parametrelere bakan ve neyin ne adar calistigini
            // bir timer mekanizmasi     
            var watch=Stopwatch.StartNew();
            try 
            {
                // path endpoint bilgisi
                // request bilgisi
                string message="[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
                _loggerService.Write(message);
                await _next(context);
                watch.Stop();

                // response bilgisi
                message = "[Response] HTTP " + context.Request.Method +" - " + context.Request.Path+" responded "  
                +" Status Code: "+ context.Response.StatusCode+ " in " +watch.Elapsed.TotalMilliseconds +" ms";
                _loggerService.Write(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context,ex,watch);

            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
            context.Response.ContentType="application/json";

            string message="[Error] HTTP " + context.Request.Method +" - " +" Status Code: "+ context.Response.StatusCode +
            " - Error Message : " + ex.Message + " in " +watch.Elapsed.TotalMilliseconds +" ms";
            _loggerService.Write(message);

            // exception objesinin geriye json olarak donmesi gerekir
            // serilaize edilmesi gerekir
           var result= JsonConvert.SerializeObject(new {error=ex.Message},Formatting.None);
           return context.Response.WriteAsync(result);
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