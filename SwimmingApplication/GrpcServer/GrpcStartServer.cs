using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using persistence;
using services;
using SwimmingApplication;

namespace GrpcServer
{
    public class GrpcStartServer
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Listen on HTTP port 5000 using HTTP/2 (required for gRPC)
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.ListenLocalhost(5000, listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http2;
                        });
                    });

                    webBuilder.UseUrls("http://localhost:5000");

                    webBuilder.ConfigureServices(services =>
                    {
                        var props = new Dictionary<string, string>
                        {
                            { "ConnectionString", "Data Source=D:\\sem4\\project PA c\\SwimmingApplication\\swimming.db;Cache=Shared" }
                        };

                        IUserRepository userRepo = new UserRepository(props);
                        IParticipantRepository participantRepo = new ParticipantRepository(props);
                        IRaceRepository raceRepo = new RaceRepository(props);
                        IParticipantRaceRepository prRepo = new ParticipantRaceRepository(props);

                        ISwimmingServices swimmingService = new SwimmingServerImpl(userRepo, participantRepo, raceRepo, prRepo);

                        services.AddSingleton(swimmingService);
                        services.AddGrpc();
                    });

                    webBuilder.Configure(app =>
                    {
                        app.UseRouting();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGrpcService<GrpcSwimmingServiceImpl>();
                        });
                    });
                });
    }
}
