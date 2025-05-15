using services;
using model;
using Grpc.Core;
namespace GrpcServer;

public class GrpcSwimmingServiceImpl : SwimmingService.SwimmingServiceBase
    {
        private readonly ISwimmingServices _swimmingService;

        public GrpcSwimmingServiceImpl(ISwimmingServices swimmingService)
        {
            _swimmingService = swimmingService;
        }

        public override Task<UserResponse> Login(LoginRequest request, ServerCallContext context)
        {
            try
            {
                // Map from gRPC request to domain model
                var loginUser = new model.User("", request.Email, request.Password);
        
                // Perform login using domain logic
                var user = _swimmingService.Login(loginUser, null);

                // Map result to gRPC User message
                return Task.FromResult(new UserResponse
                {
                    User = new GrpcServer.User
                    {
                        Id = user.ID,
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password
                    }
                });
            }
            catch (ISwimmingException ex)
            {
                return Task.FromResult(new UserResponse
                {
                    Error = ex.Message
                });
            }
        }

        public override Task<RaceList> GetAllRaces(Empty request, ServerCallContext context)
        {
            var races = _swimmingService.GetAllRaces()
                .Select(r => new Race
                {
                    Id = r.ID,
                    Style = r.Style,
                    Distance = r.Distance,
                    NrOfParticipants = r.NrOfParticipants
                });

            var list = new RaceList();
            list.Races.AddRange(races);
            return Task.FromResult(list);
        }

        public override Task<ParticipantList> GetAllParticipants(Empty request, ServerCallContext context)
        {
            var participants = _swimmingService.GetAllParticipants()
                .Select(p => new Participant
                {
                    Id = p.ID,
                    Name = p.Name,
                    Age = p.Age,
                    UserId = p.UserID
                });

            var list = new ParticipantList();
            list.Participants.AddRange(participants);
            return Task.FromResult(list);
        }

        public override Task<RegistrationList> GetAllRegistrations(Empty request, ServerCallContext context)
        {
            var registrations = _swimmingService.GetAllRegistrations()
                .Select(r => new ParticipantRace
                {
                    Id = r.ID,
                    Participant = new Participant
                    {
                        Id = r.ParticipantId.ID,
                        Name = r.ParticipantId.Name,
                        Age = r.ParticipantId.Age,
                        UserId = r.ParticipantId.UserID
                    },
                    Race = new Race
                    {
                        Id = r.RaceId.ID,
                        Style = r.RaceId.Style,
                        Distance = r.RaceId.Distance,
                        NrOfParticipants = r.RaceId.NrOfParticipants
                    }
                });

            var list = new RegistrationList();
            list.Registrations.AddRange(registrations);
            return Task.FromResult(list); 
        }
        
        public override Task<Empty> AddRegistration(ParticipantRace request, ServerCallContext context)
    {
        var participant = new model.Participant(request.Participant.Name, request.Participant.Age, request.Participant.UserId)
        {
            ID = request.Participant.Id
        };

        var race = new model.Race(request.Race.Distance, request.Race.Style, request.Race.NrOfParticipants)
        {
            ID = request.Race.Id
        };

        var registration = new model.ParticipantRace(participant, race)
        {
            ID = request.Id
        };

        _swimmingService.AddRegistration(registration);

        return Task.FromResult(new Empty());
    }

    public override Task<LogoutResponse> Logout(LogoutRequest request, ServerCallContext context)
    {
        try
        {
            var user = new model.User(request.Name, request.Email, request.Password)
            {
                ID = request.Id
            };

            _swimmingService.Logout(user, null);

            return Task.FromResult(new LogoutResponse { Success = true });
        }
        catch (ISwimmingException ex)
        {
            return Task.FromResult(new LogoutResponse
            {
                Success = false,
                Error = ex.Message
            });
        }
    }

    public override Task<Participant> GetParticipantById(ParticipantByIdRequest request, ServerCallContext context)
    {
        var participant = _swimmingService.GetParticipantById(request.Id);

        var response = new Participant
        {
            Id = participant.ID,
            Name = participant.Name,
            Age = participant.Age,
            UserId = participant.UserID
        };

        return Task.FromResult(response);
    }

    public override Task<Race> GetRaceById(RaceByIdRequest request, ServerCallContext context)
    {
        var race = _swimmingService.GetRaceById(request.Id);

        var response = new Race
        {
            Id = race.ID,
            Style = race.Style,
            Distance = race.Distance,
            NrOfParticipants = race.NrOfParticipants
        };

        return Task.FromResult(response);
    }
    
    public override Task<UserResponse> AddUser(User request, ServerCallContext context)
    {
        try
        {
            // Map gRPC User to domain User
            var userToAdd = new model.User(request.Name, request.Email, request.Password)
            {
                ID = request.Id
            };

            var addedUser = _swimmingService.AddUser(userToAdd); // This method must be implemented in ISwimmingServices

            // Map domain User back to gRPC User
            return Task.FromResult(new UserResponse
            {
                User = new User
                {
                    Id = addedUser.ID,
                    Name = addedUser.Name,
                    Email = addedUser.Email,
                    Password = addedUser.Password
                }
            });
        }
        catch (ISwimmingException ex)
        {
            return Task.FromResult(new UserResponse
            {
                Error = ex.Message
            });
        }
    }

    public override Task<LoggedUsers> GetLoggedUsers(Empty request, ServerCallContext context)
    {
        var users = _swimmingService.GetLoggedUsers()
            .Select(u => new User
            {
                Id = u.ID,
                Name = u.Name,
                Email = u.Email,
                Password = u.Password
            });

        var list = new LoggedUsers();
        list.Users.AddRange(users);
        return Task.FromResult(list);
    }
}
