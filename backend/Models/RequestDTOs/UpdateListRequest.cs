namespace backend.Models;

public class UpdateListRequest
{
    public required int UserId { get; set; }
    public required string Category { get; set; }
    public required int Position { get; set; }

    public required string Item { get; set; }
    public required string Action { get; set; }
}