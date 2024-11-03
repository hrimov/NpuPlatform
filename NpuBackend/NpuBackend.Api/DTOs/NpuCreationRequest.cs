namespace NpuBackend.Api.DTOs;

public class NpuCreationRequest
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required IFormFile ImageFile { get; set; }
    public List<Guid> ElementIds { get; set; } = [];
}
