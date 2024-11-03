namespace NpuBackend.Domain.Models;

public class Element
{
    public Guid ElementId { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
}
