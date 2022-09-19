using System;

namespace WebApi.Services
{
    public class DbLoggerService : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[DbLogger]");
        }
    }
    
}