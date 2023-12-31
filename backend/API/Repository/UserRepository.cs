using API.Data;
using API.Models;

namespace API.Repository;

public class UserRepository : IUserRepository
{
    private TopTenContext _context;

    public UserRepository(TopTenContext context)
    {
        _context = context;
    }
    public User? GetById(int Id) => _context.Users.Find(Id);
    public User? GetByEmail(string email) => _context.Users.FirstOrDefault(x => x.Email == email);
    public List<User> GetAll() => _context.Users.ToList();
    public void Delete(User user) => _context.Users.Remove(user);
    public void Save() => _context.SaveChanges();
}