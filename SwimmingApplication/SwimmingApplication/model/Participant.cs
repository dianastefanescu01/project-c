namespace SwimmingApplication.model;

public class Participant : Entity<int>
{
    private String Name { get; set; }
    private int Age { get; set; }
    private List<Race> RaceList { get; set; }

    public Participant(String name, int age, List<Race> races)
    {
        Name = name;
        Age = age;
        RaceList = races;
    }

    public override String ToString()
    {
        return ID.ToString() + ',' + Name + ',' + Age + ',' + RaceList;
    }
}