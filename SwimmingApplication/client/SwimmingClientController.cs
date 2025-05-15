using log4net;
using model;
using services;
using SwimmingApplication.forms;

namespace SwimmingApplication;

public class SwimmingClientController : ISwimmingObserver
{
    public event EventHandler<SwimmingUserEventArgs> updateEvent;
    private ISwimmingServices _services;
    private static readonly ILog log = LogManager.GetLogger(typeof(Login));
    private User _user;

    public SwimmingClientController(ISwimmingServices services)
    {
        _services = services;
        _user = null;
    }
    
    public void login(string email, string password)
    {
        User user=new User(email, password);
        user = _services.Login(user,this);
        log.Debug("Login successful");
        _user = user;
        log.Debug("Current volunteer"+user.Email);
    }

    public void logout()
    {
        log.Debug("Logout CTRL");
        _services.Logout(_user,this);
        _user = null;
    }

    public void register(User user)
    {
        log.Debug("Register CTRL");
        _services.AddUser(user);
    }
    
    public IList<Race> getRaces()
    {
        return  _services.GetAllRaces();
    }

    public IList<Participant> getParticipants()
    {
        return _services.GetAllParticipants();
    }

    public IList<ParticipantRace> getRegistrations()
    {
        return _services.GetAllRegistrations();
    }

    public Race getRaceById(int id)
    {
        return _services.GetRaceById(id);
    }

    public Participant getParticipantById(int id)
    {
        return _services.GetParticipantById(id);
    }

    public void addRegistration(ParticipantRace registration)
    {
        _services.AddRegistration(registration);
    }
    
    public void RegistrationMade(ParticipantRace registration)
    {
        SwimmingUserEventArgs userEventArgs=new SwimmingUserEventArgs(SwimmingUserEvent.AddRegistration, registration);
        log.Debug("Registration received");
        OnUserEvent(userEventArgs);
    }

    public User addUser(User user)
    {
        return _services.AddUser(user);
    }
    
    protected virtual void OnUserEvent(SwimmingUserEventArgs e)
    {
        updateEvent?.Invoke(this, e);
        log.Debug("Update Event called");
    }
}