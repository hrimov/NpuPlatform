namespace NpuBackend.Services.Interfaces;

public interface IBlobStorageService
{
    Task<string> UploadImageAsync(string fileName, Stream fileStream);
    string GetImageUrl(string fileName);
}
