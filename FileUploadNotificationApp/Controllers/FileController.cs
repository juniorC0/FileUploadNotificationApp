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
            var storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=fileblobstoragename;AccountKey=rvTge/IhjtzRUodvQpgSQkxKaj6bALnamoo3VPrOfzveO5YPTHj7g4BXBsmd2DwdF30emRVs+wPD+ASt1ptX5A==;EndpointSuffix=core.windows.net");
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
