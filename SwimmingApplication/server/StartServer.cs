using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Sockets;
using System.Reflection;
using log4net;
using log4net.Config;
using networking;
using networking.protocol;
using persistence;
using services;
using SwimmingApplication;

namespace SwimmingServerApp;

class StartServer
{
    private static int DEFAULT_PORT = 55556;
    private static readonly string DEFAULT_IP = "127.0.0.1";
    private static readonly ILog log = LogManager.GetLogger(typeof(StartServer));

    static void Main(string[] args)
    {
        var logRepo = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepo, new FileInfo("log4net.config"));

        log.Info("Starting Swimming server...");
        int port = DEFAULT_PORT;
        string ip = DEFAULT_IP;

        string? portStr = ConfigurationManager.AppSettings["port"];
        if (!int.TryParse(portStr, out port))
        {
            log.Warn($"Invalid or missing port config. Using default: {DEFAULT_PORT}");
            port = DEFAULT_PORT;
        }

        string? ipStr = ConfigurationManager.AppSettings["ip"];
        if (!string.IsNullOrEmpty(ipStr))
        {
            ip = ipStr;
        }

        string connString = GetConnectionStringByName("swimming");
        log.Info($"Using DB connection: {connString}");

        var props = new Dictionary<string, string> { { "ConnectionString", connString } };

        var userRepo = new UserRepository(props);
        var participantRepo = new ParticipantRepository(props);
        var raceRepo = new RaceRepository(props);
        var registrationRepo = new ParticipantRaceRepository(props);

        var serviceImpl = new SwimmingServerImpl(userRepo, participantRepo, raceRepo, registrationRepo);

        var server = new SerialSwimmingServer(ip, port, serviceImpl);
        server.Start();

        log.Info("Swimming server started. Press <Enter> to stop...");
        Console.ReadLine();
    }

    private static string GetConnectionStringByName(string name)
    {
        return ConfigurationManager.ConnectionStrings[name]?.ConnectionString ?? string.Empty;
    }
}

public class SerialSwimmingServer : ConcurrentServer
{
    private readonly ISwimmingServices _service;

    public SerialSwimmingServer(string host, int port, ISwimmingServices service) : base(host, port)
    {
        _service = service;
        Console.WriteLine("SerialSwimmingServer initialized...");
    }

    protected override Thread createWorker(TcpClient client)
    {
        var worker = new SwimmingClientWorkerObject(_service, client);
        return new Thread(worker.Run);
    }
}
