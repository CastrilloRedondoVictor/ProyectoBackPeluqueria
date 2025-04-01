using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ProyectoBackPeluqueria.Models;

namespace ProyectoBackPeluqueria.Services
{
    public class ServiceStorageBlobs
    {
        private BlobServiceClient _blobServiceClient;

        public ServiceStorageBlobs(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<List<string>> GetContainersAsync()
        {
            List<string> containers = new List<string>();
            await foreach (BlobContainerItem container in _blobServiceClient.GetBlobContainersAsync())
            {
                containers.Add(container.Name);
            }
            return containers;
        }

        public async Task CreateContainerAsync(string containerName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
        }

        public async Task DeleteContainerAsync(string containerName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.DeleteIfExistsAsync();
        }


        public async Task<List<BlobModel>> GetBlobsAsync(string containerName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            List<BlobModel> blobs = new List<BlobModel>();
            await foreach (BlobItem blob in containerClient.GetBlobsAsync())
            {
                BlobModel model = new BlobModel
                {
                    Nombre = blob.Name,
                    Url = $"{containerClient.Uri}/{blob.Name}",
                    Container = containerName
                };
                blobs.Add(model);
            }
            return blobs;
        }

        public async Task DeleteBlobAsync(string containerName, string blobName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task UploadBlobAsync(string containerName, string blobName, Stream contenido)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(contenido, true);
        }
    }
}
