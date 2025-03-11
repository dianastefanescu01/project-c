using SwimmingApplication.model;

namespace SwimmingApplication.repository;

public interface IRepository<ID, E> where E: Entity<ID>
{
    IEnumerable<E> FindAll();
    void Add(E entity);
    void Delete(ID id);
    E FindByID(ID id);
}