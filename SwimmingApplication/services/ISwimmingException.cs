namespace services;

public class ISwimmingException : Exception
{
    public ISwimmingException(): base(){}
    public ISwimmingException(string msg) : base(msg) {}
    public ISwimmingException(string msg, Exception ex) : base(msg, ex){}
}