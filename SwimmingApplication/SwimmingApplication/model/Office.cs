namespace SwimmingApplication.model;

public class Office : Entity<int>
{
    private String Name { get; set; }
    private String Email { get; set; }
    private String Password { get; set; }

    public Office(String name, String email, String password) {
        Name = name;
        Email = email;
        Password = password;
    }

    public override String ToString()
    {
        return ID.ToString() + ',' + Name + ',' + Email + ',' + Password;
    }
}