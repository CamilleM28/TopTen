using backend.Models;

namespace backend.Repository;

public interface IUserRepository
{
    User? GetById(int Id);
    User? GetByEmail(string email);
    List<User> GetAll();
    void Delete(User user);
    void Save();
}