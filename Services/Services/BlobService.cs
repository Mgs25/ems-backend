using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ems_backend.Models;

namespace ems_backend.Services
{
    public class BlobService : IBlobService
    {
        private readonly IConfiguration _config;
        private string blobConnectionString;
        private string blobContainer;
        public BlobService(IConfiguration config)
        {
            _config = config;
            blobConnectionString = _config.GetConnectionString("BlobConnectionString");
            blobContainer = _config.GetConnectionString("BlobContainer");
        }
        public void DeleteFileBlob(string blobName)
        {
            throw new NotImplementedException();
        }

        public void UploadFileBlob(UploadFileRequest request)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(blobConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(blobContainer);

            var blobClient = containerClient.GetBlobClient(request.FileName);

            blobClient.Upload(request.FilePath, new BlobHttpHeaders {
                ContentType = MimeMapping.MimeUtility.GetMimeMapping(request.FilePath)
            });
        }

        private string GetContentType(string fileName)

        {
            string contentType = "application/octetstream";

            string ext = System.IO.Path.GetExtension(fileName).ToLower();

            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);

            if (registryKey != null && registryKey.GetValue("Content Type") != null)

                contentType = registryKey.GetValue("Content Type").ToString();

            return contentType;

        }

    }
}