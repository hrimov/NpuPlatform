using System.Text;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using NpuBackend.Services.Interfaces;

namespace NpuBackend.Services.Implementations
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobContainerClient _containerClient;
        private readonly string _storageBaseUrl;

        public BlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:BlobStorage"];
            var containerName = configuration["AzureStorage:ContainerName"];
            _containerClient = new BlobContainerClient(connectionString, containerName);
            _storageBaseUrl = configuration["AzureStorage:BaseUrl"];
        }

        public async Task<string> UploadImageAsync(string fileName, Stream fileStream)
        {
            var sanitizedFileName = SanitizeFileName(fileName);
            var blobClient = _containerClient.GetBlobClient(sanitizedFileName);
            await blobClient.UploadAsync(fileStream, overwrite: true);
            return fileName;
        }

        private static string SanitizeFileName(string fileName)
        {
            var sanitized = new StringBuilder();
            foreach (var c in fileName)
            {
                if (char.IsLetterOrDigit(c) || c == '-' || c == '_' || c == '.' || c == '/')
                {
                    sanitized.Append(c);
                }
                else
                {
                    sanitized.Append('_');
                }
            }

            return sanitized.ToString();
        }
        
        public string GetImageUrl(string fileName)
        {
            return $"{_storageBaseUrl}/{_containerClient.Name}/{fileName}";
        }
    }
}