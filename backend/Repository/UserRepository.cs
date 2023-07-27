using backend.Data;
using backend.Models;

namespace backend.Repository;

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
}