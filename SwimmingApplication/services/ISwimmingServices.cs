using model;

namespace services;

public interface ISwimmingServices
{
    User Login(User user, ISwimmingObserver swimmingObserver);
    void Logout(User user, ISwimmingObserver swimmingObserver);
    void AddRegistration(ParticipantRace registration);
    List<ParticipantRace> GetAllRegistrations();
    List<Participant> GetAllParticipants();
    Participant GetParticipantById(int id);
    List<Race> GetAllRaces();
    Race GetRaceById(int id);
    List<User> GetLoggedUsers();
    User AddUser(User user);
}