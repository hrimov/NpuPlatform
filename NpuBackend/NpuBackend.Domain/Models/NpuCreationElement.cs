namespace NpuBackend.Domain.Models;

public class NpuCreationElement
{
    public Guid NpuCreationId { get; set; }
    public NpuCreation NpuCreation { get; set; }
    public Guid ElementId { get; set; }
    public Element Element { get; set; }
}
