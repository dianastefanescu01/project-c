using System.Collections.Generic;
using model;

namespace persistence;

public interface IRepository<ID, E> where E: Entity<ID>
{
    IEnumerable<E> FindAll();
    void Add(E entity);
    void Delete(ID id);
    void Update(E entity);
    E FindByID(ID id);
}