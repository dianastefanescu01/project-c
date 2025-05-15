using model;

namespace persistence;

public interface IUserRepository : IRepository<int, User>
{
    User GetUserByEmailAndPassword(string email, string password);
}