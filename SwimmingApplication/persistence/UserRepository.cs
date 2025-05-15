using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using Microsoft.Data.Sqlite;
using model;

namespace persistence;

public class UserRepository : IUserRepository
{
    private static readonly ILog log = LogManager.GetLogger(typeof(UserRepository));
    private readonly IDictionary<string, string> props;

    public UserRepository(IDictionary<string, string> props)
    {
        this.props = props;
    }

    public IEnumerable<User> FindAll()
    {
        log.Info("Retrieving all users from the database.");
        var users = new List<User>();

        using (var conn = DBUtils.GetConnection(props))
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = "SELECT * FROM Users";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var user = new User(
                        reader.GetString(reader.GetOrdinal("username")),
                        reader.GetString(reader.GetOrdinal("email")),
                        reader.GetString(reader.GetOrdinal("password"))
                    )
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("user_id"))
                    };
                    users.Add(user);
                }
            }
        }

        log.Info("Retrieved all users.");
        return users;
    }

    public void Add(User entity)
    {
        log.Info("Adding user to the database.");

        using (var conn = DBUtils.GetConnection(props))
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = "INSERT INTO Users (username, email, password) VALUES (@username, @email, @password)";

            var paramUsername = cmd.CreateParameter();
            paramUsername.ParameterName = "@username";
            paramUsername.Value = entity.Name;
            cmd.Parameters.Add(paramUsername);

            var paramEmail = cmd.CreateParameter();
            paramEmail.ParameterName = "@email";
            paramEmail.Value = entity.Email;
            cmd.Parameters.Add(paramEmail);

            var paramPassword = cmd.CreateParameter();
            paramPassword.ParameterName = "@password";
            paramPassword.Value = entity.Password;
            cmd.Parameters.Add(paramPassword);

            cmd.ExecuteNonQuery();

            using (var idCmd = conn.CreateCommand())
            {
                idCmd.CommandText = "SELECT last_insert_rowid();";
                entity.ID = Convert.ToInt32(idCmd.ExecuteScalar());
            }
        }

        log.Info("User added.");
    }

    public void Update(User entity)
    {
        log.InfoFormat("Updating user with ID {0}", entity.ID);

        using (var conn = DBUtils.GetConnection(props))
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = @"
                UPDATE Users 
                SET username = @username, email = @email, password = @password 
                WHERE user_id = @user_id";

            var paramUsername = cmd.CreateParameter();
            paramUsername.ParameterName = "@username";
            paramUsername.Value = entity.Name;
            cmd.Parameters.Add(paramUsername);

            var paramEmail = cmd.CreateParameter();
            paramEmail.ParameterName = "@email";
            paramEmail.Value = entity.Email;
            cmd.Parameters.Add(paramEmail);

            var paramPassword = cmd.CreateParameter();
            paramPassword.ParameterName = "@password";
            paramPassword.Value = entity.Password;
            cmd.Parameters.Add(paramPassword);

            var paramId = cmd.CreateParameter();
            paramId.ParameterName = "@user_id";
            paramId.Value = entity.ID;
            cmd.Parameters.Add(paramId);

            cmd.ExecuteNonQuery();
        }

        log.Info("User updated.");
    }

    public void Delete(int id)
    {
        log.InfoFormat("Deleting user with ID: {0}", id);

        using (var conn = DBUtils.GetConnection(props))
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = "DELETE FROM Users WHERE user_id = @id";

            var paramId = cmd.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            cmd.Parameters.Add(paramId);

            cmd.ExecuteNonQuery();
        }

        log.Info("User deleted.");
    }

    public User FindByID(int id)
    {
        log.InfoFormat("Finding user by ID: {0}", id);

        using (var conn = DBUtils.GetConnection(props))
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = "SELECT * FROM Users WHERE user_id = @id";

            var paramId = cmd.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            cmd.Parameters.Add(paramId);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    var user = new User(
                        reader.GetString(reader.GetOrdinal("username")),
                        reader.GetString(reader.GetOrdinal("email")),
                        reader.GetString(reader.GetOrdinal("password"))
                    )
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("user_id"))
                    };
                    log.InfoFormat("User found: {0}", user.Name);
                    return user;
                }
            }
        }

        log.Warn("No user found with the given ID.");
        return null;
    }

    public User GetUserByEmailAndPassword(string email, string password)
    {
        log.InfoFormat("Attempting login for email: {0}", email);

        using (var conn = DBUtils.GetConnection(props))
        {
            Console.WriteLine($"[DB PATH DEBUG] Connecting to DB at: {conn}");
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Users WHERE email = @email AND password = @password";

                var paramEmail = cmd.CreateParameter();
                paramEmail.ParameterName = "@email";
                paramEmail.Value = email;
                cmd.Parameters.Add(paramEmail);

                var paramPassword = cmd.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = password;
                cmd.Parameters.Add(paramPassword);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var user = new User(
                            reader.GetString(reader.GetOrdinal("username")),
                            reader.GetString(reader.GetOrdinal("email")),
                            reader.GetString(reader.GetOrdinal("password"))
                        )
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("user_id"))
                        };
                        log.InfoFormat("User authenticated: {0}", user.Name);
                        return user;
                    }
                }
            }

            log.Warn("Login failed: No matching user.");
            return null;
        }
    }
}
