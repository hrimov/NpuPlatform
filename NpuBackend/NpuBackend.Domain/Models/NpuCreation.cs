namespace NpuBackend.Domain.Models;

public class NpuCreation
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid UserId { get; set; }
    public List<string> Tags { get; set; }
    public string ElementUsed { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Score { get; set; }
}
