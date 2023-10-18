namespace API.Models;

public class User
{
    public int Id { get; set; }

    public required string Name { get; set; }
    public required string Email { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Expires { get; set; }
    public required Lists Lists { get; set; }


}