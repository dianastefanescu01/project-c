namespace model;

public class ParticipantRace : Entity<int>
{
    public Participant ParticipantId { get; set; }
    public Race RaceId { get; set; }

    public ParticipantRace(Participant participantId, Race raceId)
    {
        ParticipantId = participantId;
        RaceId = raceId;
    }

    public override string ToString()
    {
        return ID + ',' + ParticipantId.ID.ToString() + ',' + RaceId.ID.ToString();
    }
}