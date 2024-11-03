namespace NpuBackend.Domain.Models;

public class Score
{
    public Guid UserId { get; set; }
    public Guid NpuCreationId { get; set; }
    public int Value { get; set; }
}
