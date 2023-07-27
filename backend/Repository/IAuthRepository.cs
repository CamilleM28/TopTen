using backend.Models;

namespace backend.Repository;

public interface IAuthRepository
{
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    string CreateToken(User user);

    public void Add(User user);
    void Save();
}