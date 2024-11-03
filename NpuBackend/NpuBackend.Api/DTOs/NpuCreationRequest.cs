namespace NpuBackend.Api.DTOs;

public class NpuCreationRequest
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public List<Guid> ElementIds { get; set; } = [];
}
