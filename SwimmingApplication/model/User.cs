namespace model;

[Serializable]
public class User : Entity<int>
{
    public String Name { get; set; }
    public String Email { get; set; }
    public String Password { get; set; }

    public User(String name, String email, String password) {
        Name = name;
        Email = email;
        Password = password;
    }

    public User(String email, String password)
    {
        Email = email;
        Password = password;
    }

    public User()
    {
        
    }

    public override String ToString()
    {
        return ID.ToString() + ',' + Name + ',' + Email + ',' + Password;
    }
}