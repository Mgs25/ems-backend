using Azure.Storage.Blobs;
using ems_backend.Models;
using ems_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ems_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlobController
    {
        private readonly IConfiguration _config;
        private readonly IBlobService _blob;
        public BlobController(IConfiguration config, IBlobService blob)
        {
            _config = config;
            _blob = blob;
        }

        [HttpPost("upload-file")]
        public ActionResult<string> UploadFile(UploadFileRequest request)
        {
            try
            {
                _blob.UploadFileBlob(request);
                return "File uploaded!";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}