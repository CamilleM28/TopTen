namespace backend.Models;

public class GoogleTokenRequestDto
{
    public required string Code { get; set; }
    public required string Uri { get; set; }
    public required string Id { get; set; }
    public required string Secret { get; set; }
}