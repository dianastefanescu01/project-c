using System;
using System.Collections.Generic;
using System.Web.Http;
using model;
using persistence;

namespace RaceRestApi.Controller
{
    [RoutePrefix("api/races")]
    public class RaceController : ApiController
    {
        private readonly IRaceRepository _raceRepository;

        public RaceController(IRaceRepository raceRepository)
        {
            Console.WriteLine("RaceController created");
            _raceRepository = raceRepository;
        }


        [HttpGet]
        [Route("")]
        public IEnumerable<Race> GetAll()
        {
            Console.WriteLine(">>> RaceController.GetAll() called");
            Console.WriteLine("GET all races");
            return _raceRepository.FindAll();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            Console.WriteLine($"GET race by id: {id}");
            var race = _raceRepository.FindByID(id);
            if (race == null)
                return NotFound();
            return Ok(race);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] Race race)
        {
            Console.WriteLine($"POST new race: {race}");
            if (race == null)
                return BadRequest("Race data is null");

            race.ID = 0;
            _raceRepository.Add(race);
            return Ok(race); // or Created with URI if needed
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] Race race)
        {
            Console.WriteLine($"PUT update race: {id}");
            if (race == null)
                return BadRequest("Race data is null");

            var existing = _raceRepository.FindByID(id);
            if (existing == null)
                return NotFound();

            race.ID = id;
            _raceRepository.Update(race);
            return Ok(race);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            Console.WriteLine($"DELETE race: {id}");
            var existing = _raceRepository.FindByID(id);
            if (existing == null)
                return NotFound();

            _raceRepository.Delete(id);
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
        
        [HttpGet]
        [Route("test")]
        public string Test()
        {
            Console.WriteLine(">>> Test route called");
            return "Test OK!";
        }
        
        public RaceController() : this(new RaceRepository(new Dictionary<string, string> {
            { "ConnectionString", "Data Source=D:/sem4/project PA c/SwimmingApplication/swimming.db" }
        })) { }
    }
}