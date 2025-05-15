using System.Collections.Generic;
using model;

namespace persistence;

public interface IParticipantRepository : IRepository<int, Participant>
{
    List<Participant> FindParticipantByName(string name);
}