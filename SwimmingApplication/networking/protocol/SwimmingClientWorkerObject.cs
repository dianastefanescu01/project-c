using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using log4net;
using model;
using services;

namespace networking.protocol;

public class SwimmingClientWorkerObject : ISwimmingObserver
{
    private readonly ISwimmingServices _server;
    private readonly TcpClient _connection;
    private readonly NetworkStream _networkStream;
    private volatile bool _isConnected;
    private static readonly ILog Log = LogManager.GetLogger(typeof(SwimmingClientWorkerObject));

    public SwimmingClientWorkerObject(ISwimmingServices server, TcpClient tcpClient)
    {
        _server = server;
        _connection = tcpClient;
        try
        {
            _networkStream = _connection.GetStream();
            _isConnected = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }
    }

    public void Run()
    {
        using StreamReader reader = new StreamReader(_networkStream, Encoding.UTF8);
        while (_isConnected)
        {
            try
            {
                string requestJson = reader.ReadLine();
                if (string.IsNullOrEmpty(requestJson)) continue;

                Log.DebugFormat("Received JSON request: {0}", requestJson);
                Request request = JsonSerializer.Deserialize<Request>(requestJson);
                Log.DebugFormat("Deserialized Request: {0}", request);

                Response response = HandleRequest(request);
                if (response != null)
                {
                    SendResponse(response);
                }
            }
            catch (Exception e)
            {
                Log.ErrorFormat("Run error: {0}", e.Message);
                Log.Error(e.StackTrace);
            }

            try
            {
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        try
        {
            _networkStream.Close();
            _connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error closing connection: " + e);
        }
    }

    private void SendResponse(Response response)
    {
        string jsonString = JsonSerializer.Serialize(response);
        Log.DebugFormat("Sending response: {0}", jsonString);
        lock (_networkStream)
        {
            byte[] data = Encoding.UTF8.GetBytes(jsonString + "\n");
            _networkStream.Write(data, 0, data.Length);
            _networkStream.Flush();
        }
    }

    public void RegistrationMade(ParticipantRace registration)
    {
        try
        {
            SendResponse(new Response
            {
                Type = ResponseType.UPDATE_REGISTRATION,
                Registration = registration
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }
    }

    private Response HandleRequest(Request request)
    {
        try
        {
            switch (request.Type)
            {
                case RequestType.LOGIN:
                    Log.Debug("Handling LOGIN");
                    var user = _server.Login(request.User, this);
                    return new Response { Type = ResponseType.LOGIN_SUCCESS, User = user };

                case RequestType.LOGOUT:
                    Log.Debug("Handling LOGOUT");
                    _server.Logout(request.User, this);
                    _isConnected = false;
                    return new Response { Type = ResponseType.LOGOUT_SUCCESS };

                case RequestType.REGISTER:
                    Log.Debug("Handling REGISTER_USER");
                    var newUser = _server.AddUser(request.User);
                    //return new Response { Type = ResponseType.REGISTER_SUCCESS, User = newUser };
                    return JsonProtocolUtils.createRegisterResponse(newUser);

                case RequestType.GET_RACES:
                    Log.Debug("Handling GET_RACES");
                    var races = _server.GetAllRaces();
                    //return new Response { Type = ResponseType.RACES_RETRIEVED, Races = races };
                    return JsonProtocolUtils.CreateGetAllRacesResponse(races);

                case RequestType.GET_RACE_BY_ID:
                    Log.Debug("Handling GET_RACE_BY_ID");
                    var race = _server.GetRaceById(request.Id);
                    //return new Response { Type = ResponseType.RACE_RETRIEVED, Race = race };
                    return JsonProtocolUtils.CreateGetRaceByIdResponse(race);

                case RequestType.GET_PARTICIPANTS:
                    Log.Debug("Handling GET_PARTICIPANTS");
                    var participants = _server.GetAllParticipants();
                    //return new Response { Type = ResponseType.PARTICIPANTS_RETRIEVED, Participants = participants };
                    return JsonProtocolUtils.CreateGetAllParticipantsResponse(participants);

                case RequestType.GET_PARTICIPNT_BY_ID:
                    Log.Debug("Handling GET_PARTICIPANT_BY_ID");
                    var participant = _server.GetParticipantById(request.Id);
                    //return new Response { Type = ResponseType.PARTICIPANT_RETRIEVED, Participant = participant };
                    return JsonProtocolUtils.CreateGetParticipantByIdResponse(participant);

                case RequestType.GET_REGISTRATIONS:
                    Log.Debug("Handling GET_REGISTRATIONS");
                    var regs = _server.GetAllRegistrations();
                    //return new Response { Type = ResponseType.REGISTRATIONS_RETRIEVED, Registrations = regs };
                    return JsonProtocolUtils.CreateGetAllRegistrationsResponse(regs);

                case RequestType.ADD_REGISTRATION:
                    Log.Debug("Handling ADD_REGISTRATION");
                    _server.AddRegistration(request.Registration);
                    return new Response
                    {
                        Type = ResponseType.REGISTRATION_ADDED,
                        Registration = request.Registration
                    };

                default:
                    return new Response
                    {
                        Type = ResponseType.ERROR,
                        ErrorMessage = "Unknown request type"
                    };
            }
        }
        catch (ISwimmingException ex)
        {
            Log.Error("Error in HandleRequest", ex);
            return new Response
            {
                Type = ResponseType.ERROR,
                ErrorMessage = ex.Message
            };
        }
    }
}
