namespace backend.Models;

public class UpdateListRequest
{
    public required int userId { get; set; }
    public required string category { get; set; }
    public required int position { get; set; }
    public required string item { get; set; }
    public required string action { get; set; }
}