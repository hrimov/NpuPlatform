namespace NpuBackend.Domain.Models;

public class NpuCreation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid UserId { get; set; }
    public List<NpuCreationElement> Elements { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Score> Scores { get; set; }
    public int TotalScore => Scores?.Sum(s => s.Value) ?? 0;
}
