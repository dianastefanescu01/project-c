
using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using Microsoft.Data.Sqlite;
using model;

namespace persistence;

public class ParticipantRaceRepository : IParticipantRaceRepository
{
    private static readonly ILog log = LogManager.GetLogger(typeof(ParticipantRaceRepository));
    private readonly ParticipantRepository _participantRepository;
    private readonly RaceRepository _raceRepository;
    private readonly IDictionary<string, string> props;
    
    public ParticipantRaceRepository(IDictionary<string, string> props)
    {
        this.props = props;
        _participantRepository = new ParticipantRepository(this.props);
        _raceRepository = new RaceRepository(this.props);
    }

   /* public IEnumerable<ParticipantRace> FindAll()
    {
        log.Info("Retrieving all participant races from the database.");
        List<ParticipantRace> participantRaces = new List<ParticipantRace>();

        using (var conn = DBUtils.GetConnection(props))
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM ParticipantRaces";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int prId = reader.GetInt32(0);            // pr_id
                        int participantId = reader.GetInt32(1);   // participant_id
                        int raceId = reader.GetInt32(2);          // race_id

                        Participant participant = _participantRepository.FindByID(participantId);
                        Race race = _raceRepository.FindByID(raceId);
                
                        var participantRace = new ParticipantRace(participant, race);
                        participantRace.ID = prId;
                        participantRaces.Add(participantRace);  
                    }
                }
            }
        }
        log.Info("Retrieved all participant races from the database.");
        return participantRaces;
    }

 */
   public IEnumerable<ParticipantRace> FindAll()
   {
       log.Info("Retrieving all participant races from the database.");
       List<(int prId, int participantId, int raceId)> tempData = new();

       using (var conn = DBUtils.GetConnection(props))
       using (var cmd = conn.CreateCommand())
       {
           cmd.CommandText = "SELECT pr_id, participant_id, race_id FROM ParticipantRaces";
           using (var reader = cmd.ExecuteReader())
           {
               while (reader.Read())
               {
                   int prId = reader.GetInt32(0);
                   int participantId = reader.GetInt32(1);
                   int raceId = reader.GetInt32(2);

                   tempData.Add((prId, participantId, raceId));
               }
           }
       }

       // Fetch related objects after the reader is closed
       List<ParticipantRace> result = new();
       foreach (var (prId, participantId, raceId) in tempData)
       {
           Participant participant = _participantRepository.FindByID(participantId);
           Race race = _raceRepository.FindByID(raceId);

           var participantRace = new ParticipantRace(participant, race)
           {
               ID = prId
           };

           result.Add(participantRace);
       }

       log.Info("Retrieved all participant races from the database.");
       return result;
   }

    public ParticipantRace FindByID(int id)
    {
        log.InfoFormat("Finding participant race with id: {0}", id);

        using (var conn = DBUtils.GetConnection(props))
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM ParticipantRaces WHERE pr_id=@id";
                var paramId = cmd.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                cmd.Parameters.Add(paramId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Participant participant = _participantRepository.FindByID(reader.GetInt32(reader.GetOrdinal("participant_id")));
                        Race race = _raceRepository.FindByID(reader.GetInt32(reader.GetOrdinal("race_id")));

                        var participantRace = new ParticipantRace(participant, race);
                        participantRace.ID = reader.GetInt32(reader.GetOrdinal("pr_id"));
                        log.InfoFormat("Found participant race: {0}", participantRace.ID);
                        return participantRace;
                    }
                }
            }
        }

        log.Info("No participant race found with this id.");
        return null;
    }

    public void Add(ParticipantRace entity)
    {
        log.Info("Adding participant race to the database.");

        using (var conn = DBUtils.GetConnection(props))
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    "INSERT INTO ParticipantRaces(participant_id, race_id) VALUES (@participant_id, @race_id)";

                var paramPartId = cmd.CreateParameter();
                paramPartId.ParameterName = "@participant_id";
                paramPartId.Value = entity.ParticipantId.ID;
                cmd.Parameters.Add(paramPartId);

                var paramRaceId = cmd.CreateParameter();
                paramRaceId.ParameterName = "@race_id";
                paramRaceId.Value = entity.RaceId.ID;
                cmd.Parameters.Add(paramRaceId);

                cmd.ExecuteNonQuery();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT LAST_INSERT_ROWID();";
                    entity.ID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        log.Info("Participant race added to the database.");
    }

    public void Update(ParticipantRace entity)
    {
        log.InfoFormat("Updating participant race with id: {0}", entity.ID);

        using (var conn = DBUtils.GetConnection(props))
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    "UPDATE ParticipantRaces SET participant_id=@participant_id, race_id=@race_id WHERE pr_id=@pr_id";
                
                var paramPartId = cmd.CreateParameter();
                paramPartId.ParameterName = "@participant_id";
                paramPartId.Value = entity.ParticipantId.ID;
                cmd.Parameters.Add(paramPartId);

                var paramRaceId = cmd.CreateParameter();
                paramRaceId.ParameterName = "@race_id";
                paramRaceId.Value = entity.RaceId.ID;
                cmd.Parameters.Add(paramRaceId);
                
                var paramId = cmd.CreateParameter();
                paramId.ParameterName = "@pr_id";
                paramId.Value = entity.ID;
                cmd.Parameters.Add(paramId);

                cmd.ExecuteNonQuery();
            }
        }

        log.InfoFormat("Updated participant race with id: {0}", entity.ID);
    }

    public void Delete(int id)
    {
        log.InfoFormat("Deleting participant race with id: {0}", id);

        using (var conn = DBUtils.GetConnection(props))
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM ParticipantRaces WHERE pr_id=@id";
                var paramId = cmd.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                cmd.Parameters.Add(paramId);

                cmd.ExecuteNonQuery();
            }
        }

        
        log.InfoFormat("Deleted participant race with id: {0}", id);
    }
}