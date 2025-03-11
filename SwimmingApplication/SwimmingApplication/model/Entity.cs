namespace SwimmingApplication.model;

public class Entity<TID>
{
    public TID ID { get; set; }

    public override string ToString()
    {
        return ID.ToString();
    }
}