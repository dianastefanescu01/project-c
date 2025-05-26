using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RaceRestClient
{
    class Program
    {
        static HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));

        public static async Task Main(string[] args)
        {
            client.BaseAddress = new Uri("http://localhost:8080/swimming/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            await RunMenuAsync();
        }

        static async Task RunMenuAsync()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Swimming Race REST Client ---");
                Console.WriteLine("1. Get all races");
                Console.WriteLine("2. Get race by ID");
                Console.WriteLine("3. Create race");
                Console.WriteLine("4. Update race");
                Console.WriteLine("5. Delete race");
                Console.WriteLine("0. Exit");
                Console.Write("\nSelect an option: ");

                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                try
                {
                    switch (option)
                    {
                        case 1: await GetAllRacesAsync(); break;
                        case 2: await GetRaceByIdAsync(); break;
                        case 3: await CreateRaceAsync(); break;
                        case 4: await UpdateRaceAsync(); break;
                        case 5: await DeleteRaceAsync(); break;
                        case 0: running = false; break;
                        default: Console.WriteLine("Invalid option."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static async Task GetAllRacesAsync()
        {
            var response = await client.GetAsync("races");
            if (response.IsSuccessStatusCode)
            {
                var races = await response.Content.ReadAsAsync<List<Race>>();
                Console.WriteLine("\n--- All Races ---");
                foreach (var race in races)
                    Console.WriteLine(race);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }

        static async Task GetRaceByIdAsync()
        {
            Console.Write("Enter race ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            var response = await client.GetAsync($"races/{id}");
            if (response.IsSuccessStatusCode)
            {
                var race = await response.Content.ReadAsAsync<Race>();
                Console.WriteLine("\n--- Race Details ---");
                Console.WriteLine(race);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }

        static async Task CreateRaceAsync()
        {
            var race = new Race();

            Console.Write("Enter Distance (m): ");
            race.Distance = int.TryParse(Console.ReadLine(), out int dist) ? dist : 0;

            Console.Write("Enter Style: ");
            race.Style = Console.ReadLine();

            Console.Write("Enter Number of Participants: ");
            race.NrOfParticipants = int.TryParse(Console.ReadLine(), out int num) ? num : 0;

            var response = await client.PostAsJsonAsync("races", race);
            if (response.IsSuccessStatusCode)
            {
                var createdRace = await response.Content.ReadAsAsync<Race>();
                Console.WriteLine("\n--- Race Created ---");
                Console.WriteLine(createdRace);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }

        static async Task UpdateRaceAsync()
        {
            Console.Write("Enter race ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            var getResponse = await client.GetAsync($"races/{id}");
            if (!getResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"Race with ID {id} not found.");
                return;
            }

            var race = await getResponse.Content.ReadAsAsync<Race>();

            Console.Write($"Distance ({race.Distance}): ");
            string distStr = Console.ReadLine();
            if (int.TryParse(distStr, out int dist)) race.Distance = dist;

            Console.Write($"Style ({race.Style}): ");
            string style = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(style)) race.Style = style;

            Console.Write($"Participants ({race.NrOfParticipants}): ");
            string partStr = Console.ReadLine();
            if (int.TryParse(partStr, out int part)) race.NrOfParticipants = part;

            var updateResponse = await client.PutAsJsonAsync($"races/{id}", race);
            if (updateResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Race updated:");
                Console.WriteLine(await updateResponse.Content.ReadAsAsync<Race>());
            }
            else
            {
                Console.WriteLine($"Error: {updateResponse.StatusCode}");
                Console.WriteLine(await updateResponse.Content.ReadAsStringAsync());
            }
        }

        static async Task DeleteRaceAsync()
        {
            Console.Write("Enter race ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            var response = await client.DeleteAsync($"races/{id}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Race deleted.");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }

    public class Race
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("distance")] public int Distance { get; set; }
        [JsonProperty("style")] public string Style { get; set; }
        [JsonProperty("nrOfParticipants")] public int NrOfParticipants { get; set; }

        public override string ToString()
        {
            return $"Race {{ ID={Id}, Distance={Distance}m, Style={Style}, Participants={NrOfParticipants} }}";
        }
    }

    public class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler) { }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine(">>> HTTP Request:");
            Console.WriteLine(request);
            if (request.Content != null)
                Console.WriteLine(await request.Content.ReadAsStringAsync());

            var response = await base.SendAsync(request, cancellationToken);

            Console.WriteLine(">>> HTTP Response:");
            Console.WriteLine(response);
            if (response.Content != null)
                Console.WriteLine(await response.Content.ReadAsStringAsync());

            return response;
        }
    }
}


