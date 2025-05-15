using model;

namespace networking.protocol;

public class Response
{
    public ResponseType Type { get; set; }
    public string ErrorMessage { get; set; }

    // Single entities
    public User User { get; set; }
    public Participant Participant { get; set; }
    public Race Race { get; set; }
    public ParticipantRace? Registration { get; set; }

    // Lists
    public List<Participant> Participants { get; set; }
    public List<Race> Races { get; set; }
    public List<ParticipantRace> Registrations { get; set; }
    public List<User> Users { get; set; }

    public Response()
    {
    }

    public override string ToString()
    {
        return $"[Type={Type}, ErrorMessage={ErrorMessage}, User={User}]";
    }
}