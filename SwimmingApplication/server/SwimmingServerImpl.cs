using log4net;
using model;
using persistence;
using services;

namespace SwimmingApplication;

public class SwimmingServerImpl : ISwimmingServices
{
    private readonly IUserRepository _userRepository;
    private readonly IParticipantRepository _participantRepository;
    private readonly IRaceRepository _raceRepository;
    private readonly IParticipantRaceRepository _participantRaceRepository;

    private readonly IDictionary<int, ISwimmingObserver> _loggedUsers;
    private static readonly ILog Log = LogManager.GetLogger(typeof(SwimmingServerImpl));

    public SwimmingServerImpl(
        IUserRepository userRepository,
        IParticipantRepository participantRepository,
        IRaceRepository raceRepository,
        IParticipantRaceRepository participantRaceRepository)
    {
        _userRepository = userRepository;
        _participantRepository = participantRepository;
        _raceRepository = raceRepository;
        _participantRaceRepository = participantRaceRepository;
        _loggedUsers = new Dictionary<int, ISwimmingObserver>();
    }

    public User Login(User user, ISwimmingObserver observer)
    {
        Log.Info($"Attempting login for: {user.Email}");

        var dbUser = _userRepository.GetUserByEmailAndPassword(user.Email, user.Password);
        if (dbUser == null)
            throw new ISwimmingException("Invalid credentials");

        if (_loggedUsers.ContainsKey(dbUser.ID))
            throw new ISwimmingException("User already logged in");

        _loggedUsers[dbUser.ID] = observer;
        Log.Info($"User logged in: {dbUser.Name} (ID={dbUser.ID})");

        return dbUser;
    }

    public void Logout(User user, ISwimmingObserver observer)
    {
        Log.Info($"Attempting logout for: {user.Name}");
        if (!_loggedUsers.Remove(user.ID))
            throw new ISwimmingException("User was not logged in");

        Log.Info($"User logged out: {user.Name}");
    }

    public User AddUser(User user)
    {
        Log.Info($"Registering new user: {user.Name}");
        _userRepository.Add(user);
        return user;
    }

    public void AddRegistration(ParticipantRace registration)
    {
        Log.Info($"Adding registration: P{registration.ParticipantId.ID} to R{registration.RaceId.ID}");
        _participantRaceRepository.Add(registration);
        NotifyObserversRegistration(registration);
    }

    private void NotifyObserversRegistration(ParticipantRace registration)
    {
        Log.Info("Notifying observers about new registration.");
        foreach (var observer in _loggedUsers.Values)
        {
            Task.Run(() => observer.RegistrationMade(registration));
        }
    }

    public List<ParticipantRace> GetAllRegistrations()
    {
        Log.Info("Fetching all registrations.");
        return new List<ParticipantRace>(_participantRaceRepository.FindAll());
    }

    public List<Participant> GetAllParticipants()
    {
        Log.Info("Fetching all participants.");
        return new List<Participant>(_participantRepository.FindAll());
    }

    public Participant GetParticipantById(int id)
    {
        Log.Info($"Fetching participant with ID: {id}");
        var participant = _participantRepository.FindByID(id);
        if (participant == null)
            throw new ISwimmingException("Participant not found");
        return participant;
    }

    public List<Race> GetAllRaces()
    {
        Log.Info("Fetching all races.");
        return new List<Race>(_raceRepository.FindAll());
    }

    public Race GetRaceById(int id)
    {
        Log.Info($"Fetching race with ID: {id}");
        var race = _raceRepository.FindByID(id);
        if (race == null)
            throw new ISwimmingException("Race not found");
        return race;
    }

    public List<User> GetLoggedUsers()
    {
        Log.Info($"Fetching users");
        return new List<User>(_userRepository.FindAll());
    }
}
