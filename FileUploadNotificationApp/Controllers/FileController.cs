using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace FileUploadNotificationApp.Controllers
{
    [Route("/api")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly CloudBlobClient _blobClient;

        public FileController()
        {
            var storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=fileblobstorageapp;AccountKey=Tl2xiRQOoOb6rbLH4/rudKMl59cQgO/OKD7BlgA/lbqLJCL7hzTP2aoZ+8YDPRDJIhWMwN7rwFNb+ASt3I25dw==;EndpointSuffix=core.windows.net");
            _blobClient = storageAccount.CreateCloudBlobClient();
        }

        [HttpPost]
        [Route("upload-file")]
        public async Task<IActionResult> UploadFile()
        {
            if (!Request.Form.Files.Any() || string.IsNullOrEmpty(Request.Form["email"]))
            {
                return BadRequest();
            }

            var file = Request.Form.Files[0];

            if (Path.GetExtension(file.FileName) != ".docx")
            {
                return BadRequest();
            }

            var container = _blobClient.GetContainerReference("maincontainer");
            var blob = container.GetBlockBlobReference(file.FileName);

            blob.Metadata["email"] = Request.Form["email"];

            using (var stream = file.OpenReadStream())
            {
                await blob.UploadFromStreamAsync(stream);
            }

            return Ok();
        }
    }
}
