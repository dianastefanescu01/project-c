using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Microsoft.Data.Sqlite;
namespace persistence;

public class DBUtils
{
      private static IDbConnection _instance = null;

        public static IDbConnection GetConnection(IDictionary<string, string> props)
        {
            if (_instance != null && _instance.State != ConnectionState.Closed)
            {
                Console.WriteLine("[DbUtils] Returning existing open connection");
                return _instance;
            }
            
            Console.WriteLine("[DbUtils] Creating new connection...");
            _instance = GetNewConnection(props);
            
            try
            {
                Console.WriteLine("[DbUtils] Attempting to open connection...");
                _instance.Open();
                Console.WriteLine("[DbUtils] Connection opened successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DbUtils] Failed to open connection: {ex.Message}");
                throw; // Re-throw so you see the full crash
            }

            return _instance;
        }

        private static IDbConnection GetNewConnection(IDictionary<string, string> props)
        {
            return ConnectionFactory.GetInstance().CreateConnection(props);
        }
    }

    public abstract class ConnectionFactory
    {
        protected ConnectionFactory() { }

        private static ConnectionFactory _instance;

        public static ConnectionFactory GetInstance()
        {
            if (_instance != null)
                return _instance;

            Console.WriteLine("[ConnectionFactory] Searching for connection factory...");

            var assem = Assembly.GetExecutingAssembly();
            var types = assem.GetTypes();
            foreach (var type in types)
            {
                if (type.IsSubclassOf(typeof(ConnectionFactory)))
                {
                    Console.WriteLine($"[ConnectionFactory] Found factory: {type.Name}");
                    _instance = (ConnectionFactory)Activator.CreateInstance(type);
                }
            }
            if (_instance == null)
                Console.WriteLine("[ConnectionFactory] No ConnectionFactory found!");
            return _instance;
        }

        public abstract IDbConnection CreateConnection(IDictionary<string, string> props);
    }

    public class SqliteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection CreateConnection(IDictionary<string, string> props)
        {
            Console.WriteLine("[SqliteConnectionFactory] Creating sqlite connection...");
            var connectionString = props["ConnectionString"];
            Console.WriteLine($"[SqliteConnectionFactory] Using connection string: {connectionString}");
            return new SqliteConnection(connectionString);
        }
}

