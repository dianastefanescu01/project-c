using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using Owin;
using persistence;

namespace RaceRestApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Console.WriteLine(">>> Web API is configuring...");

            var config = new HttpConfiguration();

            // Enable [Route] / [RoutePrefix] attributes
            config.MapHttpAttributeRoutes();

            // Optional: fallback default route (won't override attribute routes)
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Show found controllers
            foreach (var controller in typeof(Startup).Assembly.GetTypes())
            {
                if (typeof(ApiController).IsAssignableFrom(controller))
                    Console.WriteLine($">>> Found controller: {controller.FullName}");
            }

            // Optional: force JSON output (disables XML)
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Setup repository and DI
            string connectionString = ConfigurationManager.ConnectionStrings["swimming"]?.ConnectionString 
                ?? throw new Exception("Missing 'swimming' connection string in App.config!");

            var props = new Dictionary<string, string> { { "ConnectionString", connectionString } };
            var raceRepository = new RaceRepository(props);

            //config.DependencyResolver = new SimpleDependencyResolver(raceRepository);

            app.UseWebApi(config);

            Console.WriteLine(">>> Web API configured and running.");
        }
    }

    public class SimpleDependencyResolver : IDependencyResolver
    {
        private readonly IRaceRepository _raceRepository;

        public SimpleDependencyResolver(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }

        public object GetService(Type serviceType)
        {
            Console.WriteLine(">>> Web API is asking for: " + serviceType.FullName);

            // Explicit match: return RaceController if asked
            if (serviceType.FullName == "RaceRestApi.Controller.RaceController")
            {
                Console.WriteLine(">>> Resolving RaceController");
                return new Controller.RaceController(_raceRepository);
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Enumerable.Empty<object>();
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
        }
    }
}
