namespace SwimmingApplication.model;

public class Race : Entity<int>
{
    private int Distance { get; set; }
    private String Style { get; set; }
    private int NrOfParticipants { get; set; }

    public Race(int distance, String style, int nrParticipants) {
        Distance = distance;
        Style = style;
        NrOfParticipants = nrParticipants;
    }

    public override String ToString() {
        return ID.ToString() + ',' + Distance + ',' + Style + ',' + NrOfParticipants;
    }
}