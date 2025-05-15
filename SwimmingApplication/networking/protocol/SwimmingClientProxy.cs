using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using log4net;
using model;
using services;
#pragma warning disable SYSLIB0011

namespace networking.protocol;

public class SwimmingClientProxy : ISwimmingServices
{
    private readonly string _host;
    private readonly int _port;
    private ISwimmingObserver _client;
    private NetworkStream _stream;
    private TcpClient _connection;
    private readonly Queue<Response> _responses;
    private volatile bool _finished;
    private EventWaitHandle _waitHandle;
    private static readonly ILog Log = LogManager.GetLogger(typeof(SwimmingClientProxy));

    public SwimmingClientProxy(string host, int port)
    {
        _host = host;
        _port = port;
        _responses = new Queue<Response>();
    }

    public User Login(User user, ISwimmingObserver observer)
    {
        InitializeConnection();
        SendRequest(JsonProtocolUtils.CreateLoginRequest(user));
        Response response = ReadResponse();

        if (response.Type == ResponseType.LOGIN_SUCCESS)
        {
            _client = observer;
            return response.User;
        }
        else
        {
            CloseConnection();
            throw new ISwimmingException(response.ErrorMessage);
        }
    }

    public void Logout(User user, ISwimmingObserver observer)
    {
        SendRequest(JsonProtocolUtils.CreateLogoutRequest(user));
        Response response = ReadResponse();
        if (response.Type == ResponseType.LOGOUT_SUCCESS)
        {
            CloseConnection();
        }
        else
        {
            throw new ISwimmingException(response.ErrorMessage);
        }
    }
    
    public User AddUser(User user)
    {
        SendRequest(JsonProtocolUtils.CreateAddUserRequest(user));
        Response response = ReadResponse();
    
        if (response.Type == ResponseType.REGISTER_SUCCESS)
        {
            return response.User!;
        }
        else
        {
            throw new ISwimmingException(response.ErrorMessage);
        }
    }

    public void AddRegistration(ParticipantRace registration)
    {
        SendRequest(JsonProtocolUtils.CreateAddRegistrationRequest(registration));
        Response response = ReadResponse();
        if (response.Type != ResponseType.REGISTRATION_ADDED)
        {
            throw new ISwimmingException(response.ErrorMessage);
        }
    }

    public List<ParticipantRace> GetAllRegistrations()
    {
        SendRequest(JsonProtocolUtils.CreateGetAllRegistrationsRequest());
        Response response = ReadResponse();
        if (response.Type == ResponseType.REGISTRATIONS_RETRIEVED)
        {
            return response.Registrations;
        }
        else
        {
            throw new ISwimmingException(response.ErrorMessage);
        }
    }

    public List<Participant> GetAllParticipants()
    {
        SendRequest(JsonProtocolUtils.CreateGetAllParticipantsRequest());
        Response response = ReadResponse();
        if (response.Type == ResponseType.PARTICIPANTS_RETRIEVED)
        {
            return response.Participants;
        }
        else
        {
            throw new ISwimmingException(response.ErrorMessage);
        }
    }

    public Participant GetParticipantById(int id)
    {
        SendRequest(JsonProtocolUtils.CreateGetParticipantByIdRequest(id));
        Response response = ReadResponse();
        if (response.Type == ResponseType.PARTICIPANT_RETRIEVED)
        {
            return response.Participant!;
        }
        else
        {
            throw new ISwimmingException(response.ErrorMessage);
        }
    }

    public List<Race> GetAllRaces()
    {
        SendRequest(JsonProtocolUtils.CreateGetAllRacesRequest());
        Response response = ReadResponse();
        if (response.Type == ResponseType.RACES_RETRIEVED)
        {
            return response.Races;
        }
        else
        {
            throw new ISwimmingException(response.ErrorMessage);
        }
    }

    public List<User> GetLoggedUsers()
    {
        SendRequest(JsonProtocolUtils.CreateGetUsersRequest());
        Response response = ReadResponse();
        if (response.Type == ResponseType.USERS_RETRIEVED)
        {
            return response.Users;
        }
        else
        {
            throw new ISwimmingException(response.ErrorMessage);
        }
    }

    public Race GetRaceById(int id)
    {
        SendRequest(JsonProtocolUtils.CreateGetRaceByIdRequest(id));
        Response response = ReadResponse();
        if (response.Type == ResponseType.RACE_RETRIEVED)
        {
            return response.Race!;
        }
        else
        {
            throw new ISwimmingException(response.ErrorMessage);
        }
    }

    // === Private Communication Logic ===

    private void SendRequest(Request request)
    {
        try
        {
            lock (_stream)
            {
                string json = JsonSerializer.Serialize(request);
                byte[] data = Encoding.UTF8.GetBytes(json + "\n");
                Log.Debug($"Sending request: {json}");
                _stream.Write(data, 0, data.Length);
                _stream.Flush();
            }
        }
        catch (Exception e)
        {
            throw new ISwimmingException("Error sending request: " + e.Message);
        }
    }

    private Response ReadResponse()
    {
        Response response=null;
        try
        {
            Log.Debug("Waiting for response...");
            _waitHandle.WaitOne();
            Log.Debug("Dequeuing response...");
            lock (_responses)
            {
                response = _responses.Dequeue();
            }
        }
        catch (Exception e)
        {
            throw new ISwimmingException("Error reading response: " + e.Message);
        }

        return response;
    }

    private void InitializeConnection()
    {
        if (_connection != null && _connection.Connected)
            return;

        try
        {
            _connection = new TcpClient(_host, _port);
            _stream = _connection.GetStream();
            _finished = false;
            _waitHandle = new AutoResetEvent(false);
            StartReader();
        }
        catch (Exception e)
        {
            throw new ISwimmingException("Error initializing connection: " + e.Message);
        }
    }

    private void CloseConnection()
    {
        _finished = true;
        try
        {
            _stream.Close();
            _connection.Close();
            _waitHandle.Close();
            _client = null;
        }
        catch (Exception e)
        {
            Log.Error("Error closing connection", e);
        }
    }

    private void StartReader()
    {
        Thread readerThread = new Thread(Run);
        readerThread.Start();
    }

    public void Run()
    {
        Log.Debug("Run() started – waiting for server responses...");
        using StreamReader reader = new StreamReader(_stream, Encoding.UTF8);

        while (!_finished)
        {
            try
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    Log.Warn("Received empty line from server.");
                    continue;
                }

                Log.Debug("Raw line from server: " + line);

                Response response = JsonSerializer.Deserialize<Response>(line);
                Log.Debug("Deserialized response: " + response?.Type);

                lock (_responses)
                {
                    _responses.Enqueue(response);
                    Log.Debug("Enqueued response: " + response?.Type);
                }

                _waitHandle.Set();
                Log.Debug("Signal sent to unblock waiting thread.");
            }
            catch (Exception e)
            {
                Log.Error("Error in reader thread", e);
            }
        }
    }
}
