using model;

namespace networking.protocol;

public class Request
{
    public RequestType Type { get; set; }

    public User User { get; set; }
    public ParticipantRace Registration { get; set; }
    public Participant Participant { get; set; }
    public Race Race { get; set; }
 
    public int Id { get; set; }  

    public override string ToString()
    {
        return $"Request[Type={Type}, User={User}]";
    }
}