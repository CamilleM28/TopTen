namespace backend.Models;

public class ListsResponse
{
    public List<string>? Movies { get; set; }
    public List<string>? TV { get; set; }
    public List<string>? Music { get; set; }
    public List<string>? Books { get; set; }
}