namespace model;

public class Race : Entity<int>
{
    public int Distance { get; set; }
    public String Style { get; set; }
    public int NrOfParticipants { get; set; }

    public Race(int distance, String style, int nrParticipants) {
        Distance = distance;
        Style = style;
        NrOfParticipants = nrParticipants;
    }

    public override String ToString() {
        return Distance + ',' + Style + ',' + NrOfParticipants;
    }
}