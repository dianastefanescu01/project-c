namespace networking.protocol;
using model;

public class JsonProtocolUtils
{
    // === Request Creators ===

    public static Request CreateLoginRequest(User user)
    {
        return new Request { Type = RequestType.LOGIN, User = user };
    }

    public static Request CreateLogoutRequest(User user)
    {
        return new Request { Type = RequestType.LOGOUT, User = user };
    }
    
    public static Request CreateAddUserRequest(User user)
    {
        return new Request { Type = RequestType.REGISTER, User = user };
    }

    public static Request CreateAddRegistrationRequest(ParticipantRace registration)
    {
        return new Request { Type = RequestType.ADD_REGISTRATION, Registration = registration };
    }

    public static Request CreateGetAllRegistrationsRequest()
    {
        return new Request { Type = RequestType.GET_REGISTRATIONS };
    }

    public static Request CreateGetUsersRequest()
    {
        return new Request { Type = RequestType.GET_USERS };
    }

    public static Request CreateGetAllParticipantsRequest()
    {
        return new Request { Type = RequestType.GET_PARTICIPANTS };
    }

    public static Request CreateGetParticipantByIdRequest(int id)
    {
        return new Request { Type = RequestType.GET_PARTICIPNT_BY_ID, Id = id };
    }

    public static Request CreateGetAllRacesRequest()
    {
        return new Request { Type = RequestType.GET_RACES };
    }

    public static Request CreateGetRaceByIdRequest(int id)
    {
        return new Request { Type = RequestType.GET_RACE_BY_ID, Id = id };
    }

    // === Response Creators ===

    public static Response CreateErrorResponse(string errorMessage)
    {
        return new Response { Type = ResponseType.ERROR, ErrorMessage = errorMessage };
    }

    public static Response CreateLoginResponse(User user)
    {
        return new Response { Type = ResponseType.LOGIN_SUCCESS, User = user };
    }

    public static Response CreateLogoutResponse()
    {
        return new Response { Type = ResponseType.LOGOUT_SUCCESS };
    }
    
    public static Response createRegisterResponse(User user)
    {
        return new Response{Type = ResponseType.REGISTER_SUCCESS, User = user};
    }

    public static Response CreateAddRegistrationResponse(ParticipantRace registration)
    {
        return new Response { Type = ResponseType.REGISTRATION_ADDED, Registration = registration };
    }

    public static Response CreateGetAllRegistrationsResponse(List<ParticipantRace> registrations)
    {
        return new Response { Type = ResponseType.REGISTRATIONS_RETRIEVED, Registrations = registrations };
    }

    public static Response CreateGetAllParticipantsResponse(List<Participant> participants)
    {
        return new Response { Type = ResponseType.PARTICIPANTS_RETRIEVED, Participants = participants };
    }

    public static Response CreateGetParticipantByIdResponse(Participant participant)
    {
        return new Response { Type = ResponseType.PARTICIPANT_RETRIEVED, Participant = participant };
    }

    public static Response CreateGetAllRacesResponse(List<Race> races)
    {
        return new Response { Type = ResponseType.RACES_RETRIEVED, Races = races };
    }

    public static Response CreateGetRaceByIdResponse(Race race)
    {
        return new Response { Type = ResponseType.RACE_RETRIEVED, Race = race };
    }

    public static Response CreateGetUsersResponse()
    {
        return new Response { Type = ResponseType.USERS_RETRIEVED };
    }
}