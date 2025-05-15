namespace model;

public class Participant : Entity<int>
{
    public String Name { get; set; }
    public int Age { get; set; }
    public int UserID { get; set; }

    public Participant(String name, int age, int userId)
    {
        Name = name;
        Age = age;
        UserID = userId;
    }

    public override String ToString()
    {
        return Name + ',' + Age + ',' + UserID;
    }
}