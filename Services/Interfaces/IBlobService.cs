using ems_backend.Models;
using ems_backend.Entities;

namespace ems_backend.Services
{
    public interface IBlobService
    {
        public void UploadFileBlob(UploadFileRequest request);
        public void DeleteFileBlob(string blobName);
    }
}