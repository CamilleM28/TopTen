namespace API.Models;

public class Lists
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<string>? Movies { get; set; }
    public List<string>? TV { get; set; }
    public List<string>? Music { get; set; }
    public List<string>? Books { get; set; }
    public User User { get; set; } = null!;
}