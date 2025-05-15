using System;
using model;
using System.Collections.Generic;
using System.Data;
using log4net;
using Microsoft.Data.Sqlite;

namespace persistence;

public class ParticipantRepository : IParticipantRepository
{
    private static readonly ILog log = LogManager.GetLogger(typeof(ParticipantRepository));
    private readonly IDictionary<string, string> props;

    public ParticipantRepository(IDictionary<string, string> props)
    {
        this.props = props;
    }

    public IEnumerable<Participant> FindAll()
    {
        log.Info("Retrieving Participants from the database.");
        var participants = new List<Participant>();

        using (var conn = DBUtils.GetConnection(props))
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = "SELECT * FROM Participants";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var participant = new Participant(
                        reader.GetString(reader.GetOrdinal("name")),
                        reader.GetInt32(reader.GetOrdinal("age")),
                        reader.GetInt32(reader.GetOrdinal("user_id"))
                    );
                    participant.ID = reader.GetInt32(reader.GetOrdinal("participant_id"));
                    participants.Add(participant);
                }
            }
        }

        log.Info("Retrieved all participants.");
        return participants;
    }

    public void Add(Participant entity)
    {
        log.Info("Adding participant to the database.");

        using (var conn = DBUtils.GetConnection(props))
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = "INSERT INTO Participants(name, age, user_id) VALUES (@name, @age, @user_id)";

            var paramName = cmd.CreateParameter();
            paramName.ParameterName = "@name";
            paramName.Value = entity.Name;
            cmd.Parameters.Add(paramName);

            var paramAge = cmd.CreateParameter();
            paramAge.ParameterName = "@age";
            paramAge.Value = entity.Age;
            cmd.Parameters.Add(paramAge);

            var paramUserId = cmd.CreateParameter();
            paramUserId.ParameterName = "@user_id";
            paramUserId.Value = entity.UserID;
            cmd.Parameters.Add(paramUserId);

            cmd.ExecuteNonQuery();

            using (var idCmd = conn.CreateCommand())
            {
                idCmd.CommandText = "SELECT last_insert_rowid();";
                entity.ID = Convert.ToInt32(idCmd.ExecuteScalar());
            }
        }

        log.Info("Participant added.");
    }

    public void Update(Participant entity)
    {
        log.InfoFormat("Updating participant with ID {0}", entity.ID);

        using (var conn = DBUtils.GetConnection(props))
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = @"
                UPDATE Participants 
                SET name=@name, age=@age, user_id=@user_id 
                WHERE participant_id=@participant_id";

            var paramName = cmd.CreateParameter();
            paramName.ParameterName = "@name";
            paramName.Value = entity.Name;
            cmd.Parameters.Add(paramName);

            var paramAge = cmd.CreateParameter();
            paramAge.ParameterName = "@age";
            paramAge.Value = entity.Age;
            cmd.Parameters.Add(paramAge);

            var paramUserId = cmd.CreateParameter();
            paramUserId.ParameterName = "@user_id";
            paramUserId.Value = entity.UserID;
            cmd.Parameters.Add(paramUserId);

            var paramId = cmd.CreateParameter();
            paramId.ParameterName = "@participant_id";
            paramId.Value = entity.ID;
            cmd.Parameters.Add(paramId);

            cmd.ExecuteNonQuery();
        }

        log.Info("Participant updated.");
    }

    public void Delete(int id)
    {
        log.InfoFormat("Deleting participant with ID: {0}", id);

        using (var conn = DBUtils.GetConnection(props))
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = "DELETE FROM Participants WHERE participant_id=@id";

            var paramId = cmd.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            cmd.Parameters.Add(paramId);

            cmd.ExecuteNonQuery();
        }

        log.Info("Participant deleted.");
    }

    public Participant FindByID(int id)
    {
        log.InfoFormat("Finding participant by ID: {0}", id);

        using (var conn = DBUtils.GetConnection(props))
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = "SELECT * FROM Participants WHERE participant_id=@id";
            var paramId = cmd.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            cmd.Parameters.Add(paramId);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    var participant = new Participant(
                        reader.GetString(reader.GetOrdinal("name")),
                        reader.GetInt32(reader.GetOrdinal("age")),
                        reader.GetInt32(reader.GetOrdinal("user_id"))
                    );
                    participant.ID = reader.GetInt32(reader.GetOrdinal("participant_id"));
                    log.InfoFormat("Participant found: {0}", participant.Name);
                    return participant;
                }
            }
        }

        log.Warn("No participant found with the given ID.");
        return null;
    }

    public List<Participant> FindParticipantByName(string name)
    {
        log.InfoFormat("Finding participants by name: {0}", name);
        var participants = new List<Participant>();

        using (var conn = DBUtils.GetConnection(props))
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = "SELECT * FROM Participants WHERE name=@name";

            var paramName = cmd.CreateParameter();
            paramName.ParameterName = "@name";
            paramName.Value = name;
            cmd.Parameters.Add(paramName);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var participant = new Participant(
                        reader.GetString(reader.GetOrdinal("name")),
                        reader.GetInt32(reader.GetOrdinal("age")),
                        reader.GetInt32(reader.GetOrdinal("user_id"))
                    );
                    participant.ID = reader.GetInt32(reader.GetOrdinal("participant_id"));
                    participants.Add(participant);
                }
            }
        }

        log.Info("Participants retrieved by name.");
        return participants;
    }
}
