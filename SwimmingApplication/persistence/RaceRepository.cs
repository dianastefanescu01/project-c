using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using Microsoft.Data.Sqlite;
using model;

namespace persistence;

public class RaceRepository : IRaceRepository
{
    private static readonly ILog log = LogManager.GetLogger(typeof(RaceRepository));
    private readonly IDictionary<string, string> props;

    public RaceRepository(IDictionary<string, string> props)
    {
        this.props = props;
    }

    public IEnumerable<Race> FindAll()
    {
        log.Info("Retrieving all races from the database.");
        var races = new List<Race>();

        using (var conn = DBUtils.GetConnection(props))
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = "SELECT * FROM Races";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var race = new Race(
                        reader.GetInt32(reader.GetOrdinal("distance")),
                        reader.GetString(reader.GetOrdinal("style")),
                        reader.GetInt32(reader.GetOrdinal("nrOfParticipants"))
                    );
                    race.ID = reader.GetInt32(reader.GetOrdinal("race_id"));
                    races.Add(race);
                }
            }
        }

        log.Info("Retrieved all races.");
        return races;
    }

    public Race FindByID(int id)
    {
        log.InfoFormat("Finding race by ID: {0}", id);

        using (var conn = DBUtils.GetConnection(props))
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = "SELECT * FROM Races WHERE race_id = @id";

            var paramId = cmd.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            cmd.Parameters.Add(paramId);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    var race = new Race(
                        reader.GetInt32(reader.GetOrdinal("distance")),
                        reader.GetString(reader.GetOrdinal("style")),
                        reader.GetInt32(reader.GetOrdinal("nrOfParticipants"))
                    );
                    race.ID = reader.GetInt32(reader.GetOrdinal("race_id"));
                    log.InfoFormat("Race found: {0}", race.Style);
                    return race;
                }
            }
        }

        log.Warn("No race found with the given ID.");
        return null;
    }

    public void Add(Race entity)
    {
        log.Info("Adding race to the database.");

        using (var conn = DBUtils.GetConnection(props))
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = @"
                INSERT INTO Races (style, distance, nrOfParticipants) 
                VALUES (@style, @distance, @nrOfParticipants)";

            var paramStyle = cmd.CreateParameter();
            paramStyle.ParameterName = "@style";
            paramStyle.Value = entity.Style;
            cmd.Parameters.Add(paramStyle);

            var paramDistance = cmd.CreateParameter();
            paramDistance.ParameterName = "@distance";
            paramDistance.Value = entity.Distance;
            cmd.Parameters.Add(paramDistance);

            var paramNrPart = cmd.CreateParameter();
            paramNrPart.ParameterName = "@nrOfParticipants";
            paramNrPart.Value = entity.NrOfParticipants;
            cmd.Parameters.Add(paramNrPart);

            cmd.ExecuteNonQuery();

            using (var idCmd = conn.CreateCommand())
            {
                idCmd.CommandText = "SELECT last_insert_rowid();";
                entity.ID = Convert.ToInt32(idCmd.ExecuteScalar());
            }
        }

        log.Info("Race added.");
    }

    public void Update(Race entity)
    {
        log.InfoFormat("Updating race with ID: {0}", entity.ID);

        using (var conn = DBUtils.GetConnection(props))
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = @"
                UPDATE Races 
                SET style = @style, distance = @distance, nrOfParticipants = @nrOfParticipants 
                WHERE race_id = @race_id";

            var paramStyle = cmd.CreateParameter();
            paramStyle.ParameterName = "@style";
            paramStyle.Value = entity.Style;
            cmd.Parameters.Add(paramStyle);

            var paramDistance = cmd.CreateParameter();
            paramDistance.ParameterName = "@distance";
            paramDistance.Value = entity.Distance;
            cmd.Parameters.Add(paramDistance);

            var paramNrPart = cmd.CreateParameter();
            paramNrPart.ParameterName = "@nrOfParticipants";
            paramNrPart.Value = entity.NrOfParticipants;
            cmd.Parameters.Add(paramNrPart);

            var paramId = cmd.CreateParameter();
            paramId.ParameterName = "@race_id";
            paramId.Value = entity.ID;
            cmd.Parameters.Add(paramId);

            cmd.ExecuteNonQuery();
        }

        log.Info("Race updated.");
    }

    public void Delete(int id)
    {
        log.InfoFormat("Deleting race with ID: {0}", id);

        using (var conn = DBUtils.GetConnection(props))
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = "DELETE FROM Races WHERE race_id = @id";

            var paramId = cmd.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            cmd.Parameters.Add(paramId);

            cmd.ExecuteNonQuery();
        }

        log.Info("Race deleted.");
    }
}
