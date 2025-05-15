using System;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;
using log4net;
using log4net.Config;
using networking.protocol;
using services;
using SwimmingApplication.forms;

namespace SwimmingApplication;

public class StartClient
{
    private static readonly int DEFAULT_PORT = 55556;
    private static readonly string DEFAULT_IP = "127.0.0.1";
    private static readonly ILog Log = LogManager.GetLogger(typeof(StartClient));

    [STAThread]
    public static void Main(string[] args)
    {
        var logRepo = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepo, new FileInfo("log4net.config"));

        Log.Info("Starting Swimming Client");
        int port = DEFAULT_PORT;
        string ip = DEFAULT_IP;

        string portStr = ConfigurationManager.AppSettings["port"];
        if (!string.IsNullOrEmpty(portStr) && int.TryParse(portStr, out int parsedPort))
        {
            port = parsedPort;
        }
        else
        {
            Log.Warn($"Invalid or missing port setting. Using default: {DEFAULT_PORT}");
        }

        string ipStr = ConfigurationManager.AppSettings["ip"];
        if (!string.IsNullOrEmpty(ipStr))
        {
            ip = ipStr;
        }

        Log.InfoFormat("Connecting to server at {0}:{1}", ip, port);
        ISwimmingServices serverProxy = new SwimmingClientProxy(ip, port);

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        SwimmingClientController swimmingClientController = new SwimmingClientController(serverProxy);
        Login login = new Login(swimmingClientController);
        Application.Run(login);
    }
}