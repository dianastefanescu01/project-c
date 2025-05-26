using System;
using Microsoft.Owin.Hosting;

namespace RaceRestApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:8080/";
            
            Console.WriteLine("Starting REST API server on {0}", baseAddress);
            
            try
            {
                using (WebApp.Start<Startup>(url: baseAddress))
                {
                    Console.WriteLine("Server running at {0}", baseAddress);
                    Console.WriteLine("Press Enter to quit.");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error starting server: {0}", ex.Message);
            }
        }
    }
}