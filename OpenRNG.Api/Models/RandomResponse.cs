namespace OpenRNG.Api.Models;

public class RandomResponse
{
    public string Type { get; set; }
    public dynamic Value { get; set; }
    public DateTime GeneratedAt { get; set; }
}