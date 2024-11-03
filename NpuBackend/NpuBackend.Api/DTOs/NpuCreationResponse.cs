namespace NpuBackend.Api.DTOs;

public class NpuCreationResponseDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public Guid UserId { get; set; }
    public List<Guid> ElementIds { get; set; } = new();
    public required string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public int TotalScore { get; set; }
}
